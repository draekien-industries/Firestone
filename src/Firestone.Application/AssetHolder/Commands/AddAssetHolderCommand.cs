namespace Firestone.Application.AssetHolder.Commands;

using Contracts;
using Domain.Models;
using Services;

/// <summary>
/// A command that creates a new asset holder.
/// </summary>
public class AddAssetHolderCommand : IRequest<AssetHolderDto>
{
    /// <summary>
    /// The ID of the <see cref="FireTable" /> to add the asset holder to.
    /// </summary>
    public Guid FireTableId { get; init; }

    /// <summary>
    /// The name of the asset holder.
    /// </summary>
    public string Name { get; init; } = default!;

    /// <summary>
    /// The expected monthly income amount.
    /// </summary>
    public double ExpectedMonthlyIncome { get; init; }

    /// <summary>
    /// The planned monthly contribution amount.
    /// </summary>
    public double PlannedMonthlyContribution { get; init; }

    /// <summary>
    /// The validator for <see cref="AddAssetHolderCommand" />
    /// </summary>
    public class Validator : AbstractValidator<AddAssetHolderCommand>
    {
        /// <summary>
        /// Constructs a new <see cref="Validator" />.
        /// </summary>
        public Validator()
        {
            RuleFor(x => x.FireTableId).NotNull();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.ExpectedMonthlyIncome).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PlannedMonthlyContribution)
               .GreaterThanOrEqualTo(0)
               .LessThanOrEqualTo(x => x.ExpectedMonthlyIncome);
        }
    }

    /// <summary>
    /// The handler for <see cref="AddAssetHolderCommand" />.
    /// </summary>
    public class Handler : IRequestHandler<AddAssetHolderCommand, AssetHolderDto>
    {
        private readonly IAssetHolderRepository _assetHolderRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructs a new <see cref="Handler" />.
        /// </summary>
        /// <param name="assetHolderRepository">The <see cref="IAssetHolderRepository" /></param>
        /// <param name="mapper">The <see cref="IMapper" /></param>
        public Handler(IAssetHolderRepository assetHolderRepository, IMapper mapper)
        {
            _assetHolderRepository = assetHolderRepository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<AssetHolderDto> Handle(AddAssetHolderCommand request, CancellationToken cancellationToken)
        {
            AssetHolder assetHolder = await _assetHolderRepository.AddAsync(
                request.FireTableId,
                request.Name,
                request.ExpectedMonthlyIncome,
                request.PlannedMonthlyContribution,
                cancellationToken);

            return _mapper.Map<AssetHolderDto>(assetHolder);
        }
    }
}
