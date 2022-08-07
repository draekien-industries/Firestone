namespace Firestone.Application.LineItem.Commands;

using Assets.Services;
using Contracts;
using Domain.Models;
using Services;

/// <summary>
/// A command for adding a new line item to a FIRE table.
/// </summary>
public class AddLineItemCommand : IRequest<LineItemDto>
{
    /// <summary>
    /// The ID of the FIRE table to add the line item to.
    /// </summary>
    public Guid FireTableId { get; init; }

    /// <summary>
    /// The date the line item refers to.
    /// </summary>
    public DateTime Date { get; init; }

    /// <summary>
    /// A key value pair of the AssetHolderId and the AssetAmount.
    /// </summary>
    public IReadOnlyDictionary<Guid, double> Assets { get; init; } = new Dictionary<Guid, double>();

    /// <summary>
    /// The validator for <see cref="AddLineItemCommand" />
    /// </summary>
    public class Validator : AbstractValidator<AddLineItemCommand>
    {
        public Validator()
        {
            RuleFor(x => x.FireTableId).NotNull();
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.Assets).NotEmpty();
        }
    }

    /// <summary>
    /// The handler for <see cref="AddLineItemCommand" />
    /// </summary>
    public class Handler : IRequestHandler<AddLineItemCommand, LineItemDto>
    {
        private readonly IAssetsRepository _assetsRepository;
        private readonly ILineItemRepository _lineItemRepository;
        private readonly IMapper _mapper;

        public Handler(ILineItemRepository lineItemRepository, IAssetsRepository assetsRepository, IMapper mapper)
        {
            _lineItemRepository = lineItemRepository;
            _assetsRepository = assetsRepository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<LineItemDto> Handle(AddLineItemCommand request, CancellationToken cancellationToken)
        {
            LineItem lineItem = await _lineItemRepository.AddAsync(
                request.FireTableId,
                request.Date,
                cancellationToken);

            IEnumerable<Assets> assets = await _assetsRepository.AddRangeAsync(
                lineItem.Id,
                request.Assets,
                cancellationToken);

            lineItem.Assets = assets.ToList();

            return _mapper.Map<LineItemDto>(lineItem);
        }
    }
}
