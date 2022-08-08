namespace Firestone.Application.FireGraph.Queries;

using Contracts;
using Domain.Constants;
using Domain.Models;
using Domain.Utils;
using FireTable.Services;
using Services;
using Waystone.Common.Application.Contracts.Services;

public class GetGraphQuery : IRequest<FireGraphDto>
{
    public Guid Id { get; init; }

    public class Validator : AbstractValidator<GetGraphQuery>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotNull();
        }
    }

    public class Handler : IRequestHandler<GetGraphQuery, FireGraphDto>
    {
        private readonly IAssetsProjectionService _assetsProjectionService;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IFireTableRepository _fireTableRepository;
        private readonly IMapper _mapper;
        private readonly ITargetAdjustmentService _targetAdjustmentService;

        public Handler(
            IFireTableRepository fireTableRepository,
            IDateTimeProvider dateTimeProvider,
            IMapper mapper,
            IAssetsProjectionService assetsProjectionService,
            ITargetAdjustmentService targetAdjustmentService)
        {
            _fireTableRepository = fireTableRepository;
            _dateTimeProvider = dateTimeProvider;
            _mapper = mapper;
            _assetsProjectionService = assetsProjectionService;
            _targetAdjustmentService = targetAdjustmentService;
        }

        /// <inheritdoc />
        public async Task<FireGraphDto> Handle(GetGraphQuery request, CancellationToken cancellationToken)
        {
            FireTable table = await _fireTableRepository.GetAsync(request.Id, cancellationToken);

            List<LineItem> orderedInvestments = table.LineItems.OrderBy(lineItem => lineItem.Date).ToList();

            LineItem? firstInvestment = orderedInvestments.FirstOrDefault();

            var monthsElapsed = 0;
            int monthsUntilRetirement = table.YearsToRetirement * FirestoneValues.MonthsPerYear;
            DataPoint projectFrom = new(_dateTimeProvider.Now, 0);

            if (firstInvestment is not null)
            {
                LineItem lastInvestment = orderedInvestments.Last();
                projectFrom = new DataPoint(lastInvestment.Date, lastInvestment.AssetsTotal);
                monthsElapsed = DateUtils.GetTotalMonths(firstInvestment.Date, projectFrom.Date) + 1;
                monthsUntilRetirement -= monthsElapsed;
            }

            Task<IEnumerable<DataPoint>> getProjectedAssetsTask = _assetsProjectionService.GetProjectedAssets(
                projectFrom,
                monthsElapsed,
                monthsUntilRetirement,
                table.MonthlyNominalReturnRate,
                table.CombinedMonthlyContribution,
                cancellationToken);

            DateTime initialAdjustmentDate = firstInvestment?.Date ?? _dateTimeProvider.Now;
            double firstInvestmentAmount = firstInvestment?.AssetsTotal ?? 0;

            DataPoint initialRetirementTarget = new(
                initialAdjustmentDate,
                table.RetirementTargetBeforeInflation);

            Task<IEnumerable<DataPoint>> getRetirementTargetsTask = _targetAdjustmentService.GetAdjustedTargetsAsync(
                initialRetirementTarget,
                table.MonthsToRetirement,
                table.MonthlyInflationRate,
                cancellationToken: cancellationToken);

            double finalRetirementTarget = MathUtils.CompoundInterest(
                table.RetirementTargetBeforeInflation,
                table.YearlyInflationRate,
                FirestoneValues.MonthsPerYear,
                table.YearsToRetirement);

            double coastTargetStartingBalance = finalRetirementTarget
                                              / Math.Pow(
                                                    1.0 + table.MonthlyNominalReturnRate,
                                                    table.MonthsToRetirement);

            DataPoint initialCoastTarget = new(initialAdjustmentDate, coastTargetStartingBalance);

            Task<IEnumerable<DataPoint>> getCoastTargetsTask = _targetAdjustmentService.GetAdjustedTargetsAsync(
                initialCoastTarget,
                table.MonthsToRetirement,
                table.MonthlyNominalReturnRate,
                cancellationToken: cancellationToken);

            DataPoint initialMinimumGrowthRate = new(initialAdjustmentDate, firstInvestmentAmount);
            double minimumMonthlyContribution = table.MonthlyNominalReturnRate
                                              * (finalRetirementTarget
                                               - firstInvestmentAmount
                                               * Math.Pow(1 + table.MonthlyNominalReturnRate, table.MonthsToRetirement))
                                              / (Math.Pow(1 + table.MonthlyNominalReturnRate, table.MonthsToRetirement)
                                               - 1);

            Task<IEnumerable<DataPoint>> getMinimumGrowthTargetsTask = _targetAdjustmentService.GetAdjustedTargetsAsync(
                initialMinimumGrowthRate,
                monthsUntilRetirement,
                table.MonthlyNominalReturnRate,
                minimumMonthlyContribution,
                cancellationToken);


            await Task.WhenAll(
                getCoastTargetsTask,
                getRetirementTargetsTask,
                getMinimumGrowthTargetsTask,
                getProjectedAssetsTask);

            IEnumerable<DataPoint> coastTargets = getCoastTargetsTask.Result;
            IEnumerable<DataPoint> retirementTargets = getRetirementTargetsTask.Result;
            IEnumerable<DataPoint> minimumGrowthTargets = getMinimumGrowthTargetsTask.Result;
            IEnumerable<DataPoint> projectedAssets = getProjectedAssetsTask.Result;

            IOrderedEnumerable<DataPoint> recordedAssets = table
                                                          .LineItems.Select(
                                                               x => new DataPoint(
                                                                   x.Date,
                                                                   x.Assets.Sum(asset => asset.Amount)))
                                                          .OrderBy(x => x.Date);

            FireGraph graph = new()
            {
                Id = table.Id,
                CoastTargetStartingBalance = coastTargetStartingBalance,
                MinimumMonthlyContribution = minimumMonthlyContribution,
                RecordedAssets = recordedAssets,
                ProjectedAssets = projectedAssets,
                AdjustedTargets = new AdjustedTargets
                {
                    RetirementTargets = retirementTargets,
                    CoastTargets = coastTargets,
                    MinimumGrowthTargets = minimumGrowthTargets,
                },
            };

            return _mapper.Map<FireGraphDto>(graph);
        }
    }
}
