namespace Firestone.Application.LineItem.Queries;

using Contracts;
using Domain.Models;
using Services;

public class GetLineItemQuery : IRequest<LineItemDto>
{
    public Guid Id { get; init; }

    public class Validator : AbstractValidator<GetLineItemQuery>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotNull();
        }
    }

    public class Handler : IRequestHandler<GetLineItemQuery, LineItemDto>
    {
        private readonly ILineItemRepository _lineItemRepository;
        private readonly IMapper _mapper;

        public Handler(ILineItemRepository lineItemRepository, IMapper mapper)
        {
            _lineItemRepository = lineItemRepository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<LineItemDto> Handle(GetLineItemQuery request, CancellationToken cancellationToken)
        {
            LineItem lineItem = await _lineItemRepository.GetAsync(request.Id, cancellationToken);

            return _mapper.Map<LineItemDto>(lineItem);
        }
    }
}
