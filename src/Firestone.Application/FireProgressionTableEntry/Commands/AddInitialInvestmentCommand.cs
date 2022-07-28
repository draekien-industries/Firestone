namespace Firestone.Application.FireProgressionTableEntry.Commands;

using AutoMapper;
using Common.Contracts;
using Common.Data;
using Contracts;
using Domain.Data;
using Domain.Models;
using FireProgressionTable.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class AddInitialInvestmentCommand : IRequest<FireProgressionTableEntryDto>
{
    public Guid TableId { get; init; }

    public DateTime DateTime { get; init; }

    public IEnumerable<IndividualAssetsSnapshotDto> IndividualAssetsSnapshots { get; init; } =
        new List<IndividualAssetsSnapshotDto>();

    public class Validator : AbstractValidator<AddInitialInvestmentCommand>
    {
        public Validator()
        {
            RuleFor(x => x.TableId).NotEmpty();
            RuleFor(x => x.DateTime).NotNull();
            RuleFor(x => x.IndividualAssetsSnapshots).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<AddInitialInvestmentCommand, FireProgressionTableEntryDto>
    {
        private readonly IFirestoneDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFireProgressionTableRepository _repository;

        public Handler(IFirestoneDbContext context, IMapper mapper, IFireProgressionTableRepository repository)
        {
            _context = context;
            _mapper = mapper;
            _repository = repository;
        }

        /// <inheritdoc />
        public async Task<FireProgressionTableEntryDto> Handle(
            AddInitialInvestmentCommand request,
            CancellationToken cancellationToken)
        {
            FireProgressionTable table = await _repository.GetAsync(request.TableId, cancellationToken);

            if (table.RetirementTargetConfiguration == null)
            {
                throw new InvalidOperationException("Retirement target configuration is null");
            }

            if (table.NominalReturnRateConfiguration == null)
            {
                throw new InvalidOperationException("Nominal return rate configuration is null");
            }

            RetirementTargetModel retirementTarget = new(table.RetirementTargetConfiguration);
            NominalReturnRateModel nominalReturnRate = new(table.NominalReturnRateConfiguration);

            FireProgressionTableEntryModel initialInvestmentEntry = new(
                request.TableId,
                request.DateTime,
                retirementTarget);

            foreach (IndividualAssetsSnapshotDto snapshot in request.IndividualAssetsSnapshots)
            {
                initialInvestmentEntry.AddIndividualAssets(
                    new IndividualAssetsTotalModel(snapshot.AssetHolderId, snapshot.Value));
            }

            FireProgressionTableEntry initialInvestmentEntity = initialInvestmentEntry.ToEntity();

            await _context.FireProgressionTableEntries.AddAsync(initialInvestmentEntity, cancellationToken);
            await _context.SaveChangeAsync(cancellationToken);

            Guid initialInvestmentId = initialInvestmentEntity.Id;

            initialInvestmentEntry = new FireProgressionTableEntryModel(initialInvestmentEntity);

            retirementTarget.SetMinimumMonthlyContributionValue(initialInvestmentEntry, nominalReturnRate);
            retirementTarget.SetMinimumMonthlyGrowthRate(initialInvestmentEntry);

            table.RetirementTargetConfiguration = retirementTarget.ToEntity();

            _context.FireProgressionTables.Update(table);
            await _context.SaveChangeAsync(cancellationToken);

            initialInvestmentEntity = await _context.FireProgressionTableEntries
                                                    .Include(x => x.IndividualAssetValues)
                                                    .Include(x => x.ProjectedTotalAssetValues)
                                                    .FirstAsync(x => x.Id == initialInvestmentId, cancellationToken);

            initialInvestmentEntry = new FireProgressionTableEntryModel(initialInvestmentEntity);

            return _mapper.Map<FireProgressionTableEntryDto>(initialInvestmentEntry);
        }
    }
}
