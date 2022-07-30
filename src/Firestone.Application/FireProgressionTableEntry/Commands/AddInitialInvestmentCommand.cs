namespace Firestone.Application.FireProgressionTableEntry.Commands;

using AutoMapper;
using Common.Contracts;
using Common.Data;
using Contracts;
using Domain.Data;
using FireProgressionTable.Repositories;
using FluentValidation;
using MediatR;

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

            FireProgressionTableEntry initialInvestment = new(
                table,
                request.DateTime,
                table.RetirementTargetConfiguration);

            await _context.FireProgressionTableEntries.AddAsync(initialInvestment, cancellationToken);
            await _context.SaveChangeAsync(cancellationToken);

            IEnumerable<IndividualAssetsTotal> initialAssets = request.IndividualAssetsSnapshots.Select(
                x => new IndividualAssetsTotal(x.AssetHolderId, initialInvestment.Id, x.Value));

            await _context.IndividualAssetsTotals.AddRangeAsync(initialAssets, cancellationToken);
            await _context.SaveChangeAsync(cancellationToken);

            return _mapper.Map<FireProgressionTableEntryDto>(initialInvestment);
        }
    }
}
