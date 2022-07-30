namespace Firestone.Application.FireProgressionTable.Commands;

using AutoMapper;
using Common.Contracts;
using Common.Data;
using Contracts;
using Domain.Data;
using FluentValidation;
using MediatR;
using Repositories;

public class AddAssetHolderCommand : IRequest<FireProgressionTableDto>
{
    public Guid TableId { get; init; }

    public NewAssetHolderDto NewAssetHolder { get; init; } = default!;

    public class Validator : AbstractValidator<AddAssetHolderCommand>
    {
        public Validator()
        {
            RuleFor(x => x.TableId).NotNull();
            RuleFor(x => x.NewAssetHolder).NotNull();
            RuleFor(x => x.NewAssetHolder.Name).NotEmpty();
            RuleFor(x => x.NewAssetHolder.MonthlyIncome).GreaterThan(x => x.NewAssetHolder.PlannedMonthlyContribution);
        }
    }

    public class Handler : IRequestHandler<AddAssetHolderCommand, FireProgressionTableDto>
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
        public async Task<FireProgressionTableDto> Handle(
            AddAssetHolderCommand request,
            CancellationToken cancellationToken)
        {
            NewAssetHolderDto assetHolderDetails = request.NewAssetHolder;

            AssetHolder assetHolder = new(
                request.TableId,
                assetHolderDetails.Name,
                assetHolderDetails.MonthlyIncome,
                assetHolderDetails.PlannedMonthlyContribution);

            await _context.AssetHolders.AddAsync(assetHolder, cancellationToken);
            await _context.SaveChangeAsync(cancellationToken);

            FireProgressionTable table = await _repository.GetAsync(assetHolder.TableId, cancellationToken);

            return _mapper.Map<FireProgressionTableDto>(table);
        }
    }
}
