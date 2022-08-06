namespace Firestone.Application.FireProgressionTable.Commands;

using AutoMapper;
using Common.Repositories;
using Domain.Data;
using FluentValidation;
using MediatR;

public class PopulateTableCommand : IRequest
{
    public Guid TableId { get; init; }

    public class Validator : AbstractValidator<PopulateTableCommand>
    {
        public Validator()
        {
            RuleFor(x => x.TableId).NotNull();
        }
    }

    public class Handler : IRequestHandler<PopulateTableCommand>
    {
        private readonly IMapper _mapper;
        private readonly IFireProgressionTableRepository _repository;

        public Handler(IFireProgressionTableRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<Unit> Handle(
            PopulateTableCommand request,
            CancellationToken cancellationToken)
        {
            FireProgressionTable table = await _repository.GetAsync(request.TableId, cancellationToken);
            List<FireProgressionTableEntry> tableEntries = table.Entries.ToList();

            FireProgressionTableEntry previous = tableEntries.First(x => x.TotalAssetValues.HasValue);
            int initialMonth = tableEntries.IndexOf(previous) + 1;

            List<FireProgressionTableEntry> projections = new();

            for (int elapsedMonths = initialMonth;
                 elapsedMonths < table.RetirementTargetConfiguration.MonthsUntilRetirement;
                 elapsedMonths++)
            {
                DateTime date = previous.DateTime.AddMonths(1);

                FireProgressionTableEntry projection = new(
                    table,
                    date,
                    table.InflationRateConfiguration,
                    table.NominalReturnRateConfiguration,
                    previous);

                projections.Add(projection);
            }

            await _repository.UpdateProjectionsAsync(projections, cancellationToken);

            return default;
        }
    }
}
