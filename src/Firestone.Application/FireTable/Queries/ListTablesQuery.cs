namespace Firestone.Application.FireTable.Queries;

using Contracts;
using Domain.Models;
using Services;
using Waystone.Common.Application.Contracts.Pagination;

/// <summary>
/// A query to get a page of FIRE tables.
/// </summary>
public class ListTablesQuery : PaginatedRequest<FireTableSummaryDto>
{
    public class Handler : IRequestHandler<ListTablesQuery, PaginatedResponse<FireTableSummaryDto>>
    {
        private readonly IFireTableRepository _fireTableRepository;
        private readonly IMapper _mapper;

        public Handler(IFireTableRepository fireTableRepository, IMapper mapper)
        {
            _fireTableRepository = fireTableRepository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<PaginatedResponse<FireTableSummaryDto>> Handle(
            ListTablesQuery request,
            CancellationToken cancellationToken)
        {
            IEnumerable<FireTable> tables = await _fireTableRepository.ListAsync(
                request.Cursor,
                request.Limit,
                cancellationToken);
            int total = await _fireTableRepository.CountAsync(cancellationToken);

            PaginatedResponse<FireTableSummaryDto> result = new()
            {
                Results = _mapper.Map<IEnumerable<FireTableSummaryDto>>(tables),
                Total = total,
            };

            return result;
        }
    }
}
