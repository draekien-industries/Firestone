namespace Firestone.Application.FireProgressionTable.Commands;

using AutoMapper;
using Common.Contracts;
using Common.Repositories;
using Contracts;
using Domain.Data;
using FluentValidation;
using MediatR;

public class AddAssetHolderCommand : IRequest<AssetHolderDto>
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

    public class Handler : IRequestHandler<AddAssetHolderCommand, AssetHolderDto>
    {
        private readonly IAssetHolderRepository _assetHolderRepository;
        private readonly IMapper _mapper;

        public Handler(IMapper mapper, IAssetHolderRepository assetHolderRepository)
        {
            _mapper = mapper;
            _assetHolderRepository = assetHolderRepository;
        }

        /// <inheritdoc />
        public async Task<AssetHolderDto> Handle(
            AddAssetHolderCommand request,
            CancellationToken cancellationToken)
        {
            AssetHolder result = await _assetHolderRepository.AddAsync(
                request.TableId,
                request.NewAssetHolder,
                cancellationToken);

            return _mapper.Map<AssetHolderDto>(result);
        }
    }
}
