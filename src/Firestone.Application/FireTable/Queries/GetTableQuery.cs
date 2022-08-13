namespace Firestone.Application.FireTable.Queries;

using Contracts;
using Domain.Models;
using Services;

public class GetTableQuery : IRequest<FireTableDto>
{
    public Guid Id { get; init; }

    public class Validator : AbstractValidator<GetTableQuery>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotNull();
        }
    }

    public class Handler : IRequestHandler<GetTableQuery, FireTableDto>
    {
        private readonly IFireTableRepository _fireTableRepository;
        private readonly IMapper _mapper;

        public Handler(IFireTableRepository fireTableRepository, IMapper mapper)
        {
            _fireTableRepository = fireTableRepository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<FireTableDto> Handle(GetTableQuery request, CancellationToken cancellationToken)
        {
            FireTable table = await _fireTableRepository.GetAsync(request.Id, cancellationToken);

            foreach (LineItem lineItem in table.LineItems)
            {
                List<AssetHolder> assetHoldersWithoutEntry =
                    table.AssetHolders.Where(ah => !lineItem.Assets.Select(a => a.AssetHolderId).Contains(ah.Id))
                         .ToList();

                if (!assetHoldersWithoutEntry.Any()) continue;

                foreach (Assets emptyAssets in assetHoldersWithoutEntry.Select(
                             ah => new Assets
                             {
                                 AssetHolderId = ah.Id,
                                 LineItemId = lineItem.Id,
                                 Amount = 0,
                             }))
                {
                    lineItem.Assets.Add(emptyAssets);
                }
            }

            var result = _mapper.Map<FireTableDto>(table);

            return result;
        }
    }
}
