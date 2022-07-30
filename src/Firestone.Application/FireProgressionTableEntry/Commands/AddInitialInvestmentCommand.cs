namespace Firestone.Application.FireProgressionTableEntry.Commands;

using AutoMapper;
using Common.Contracts;
using Common.Repositories;
using Contracts;
using Domain.Data;
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
        private readonly IIndividualAssetTotalsRepository _assetsRepository;
        private readonly IMapper _mapper;
        private readonly IFireProgressionTableRepository _repository;
        private readonly IFireProgressionTableEntryRepository _tableEntryRepository;

        public Handler(
            IMapper mapper,
            IFireProgressionTableRepository repository,
            IFireProgressionTableEntryRepository tableEntryRepository,
            IIndividualAssetTotalsRepository assetsRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _tableEntryRepository = tableEntryRepository;
            _assetsRepository = assetsRepository;
        }

        /// <inheritdoc />
        public async Task<FireProgressionTableEntryDto> Handle(
            AddInitialInvestmentCommand request,
            CancellationToken cancellationToken)
        {
            FireProgressionTable table = await _repository.GetAsync(request.TableId, cancellationToken);

            FireProgressionTableEntry initialInvestment =
                await _tableEntryRepository.AddInitialInvestmentAsync(table, request, cancellationToken);

            await _assetsRepository.AddAsync(
                initialInvestment.Id,
                request.IndividualAssetsSnapshots,
                cancellationToken);

            return _mapper.Map<FireProgressionTableEntryDto>(initialInvestment);
        }
    }
}
