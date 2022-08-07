namespace Firestone.Application.Assets.Queries;

using Contracts;
using Domain.Models;
using Services;

/// <summary>
/// A query for getting an assets record by it's ID.
/// </summary>
public class GetAssetsQuery : IRequest<AssetsDto>
{
    /// <summary>
    /// The ID of the assets record.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// The validator for <see cref="GetAssetsQuery" />
    /// </summary>
    public class Validator : AbstractValidator<GetAssetsQuery>
    {
        /// <summary>
        /// Creates a new instance of <see cref="Validator" />.
        /// </summary>
        public Validator()
        {
            RuleFor(x => x.Id).NotNull();
        }
    }

    /// <summary>
    /// The handler for <see cref="GetAssetsQuery" />
    /// </summary>
    public class Handler : IRequestHandler<GetAssetsQuery, AssetsDto>
    {
        private readonly IAssetsRepository _assetsRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Creates a new instance of <see cref="Handler" />.
        /// </summary>
        /// <param name="assetsRepository">The <see cref="IAssetsRepository" /></param>
        /// <param name="mapper">The <see cref="IMapper" /></param>
        public Handler(IAssetsRepository assetsRepository, IMapper mapper)
        {
            _assetsRepository = assetsRepository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<AssetsDto> Handle(GetAssetsQuery request, CancellationToken cancellationToken)
        {
            Assets assets = await _assetsRepository.GetAsync(request.Id, cancellationToken);

            return _mapper.Map<AssetsDto>(assets);
        }
    }
}
