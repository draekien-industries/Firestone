namespace Firestone.Application.AssetHolder.Queries;

using Contracts;
using Domain.Models;
using Services;

/// <summary>
/// A query for getting an asset holder by their ID.
/// </summary>
public class GetAssetHolderQuery : IRequest<AssetHolderDto>
{
    /// <summary>
    /// The ID of the asset holder.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// The validator for <see cref="GetAssetHolderQuery" />
    /// </summary>
    public class Validator : AbstractValidator<GetAssetHolderQuery>
    {
        /// <summary>
        /// Constructs the validator.
        /// </summary>
        public Validator()
        {
            RuleFor(x => x.Id).NotNull();
        }
    }

    /// <summary>
    /// The handler for <see cref="GetAssetHolderQuery" />
    /// </summary>
    public class Handler : IRequestHandler<GetAssetHolderQuery, AssetHolderDto>
    {
        private readonly IAssetHolderRepository _assetHolderRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructs the handler.
        /// </summary>
        /// <param name="assetHolderRepository">The <see cref="IAssetHolderRepository" /></param>
        /// <param name="mapper">The <see cref="IMapper" /></param>
        public Handler(IAssetHolderRepository assetHolderRepository, IMapper mapper)
        {
            _assetHolderRepository = assetHolderRepository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<AssetHolderDto> Handle(GetAssetHolderQuery request, CancellationToken cancellationToken)
        {
            AssetHolder result = await _assetHolderRepository.GetAsync(request.Id, cancellationToken);

            return _mapper.Map<AssetHolderDto>(result);
        }
    }
}
