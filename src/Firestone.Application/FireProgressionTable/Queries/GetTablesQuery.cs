namespace Firestone.Application.FireProgressionTable.Queries;

using AutoMapper;
using Common.Contracts;
using Common.Repositories;
using Domain.Data;
using MediatR;
using Waystone.Common.Application.Contracts.Pagination;

public class GetTablesQuery : PaginatedRequest<FireProgressionTableSummaryDto>
{
    public class Handler : IRequestHandler<GetTablesQuery, PaginatedResponse<FireProgressionTableSummaryDto>>
    {
        private readonly IMapper _mapper;
        private readonly IFireProgressionTableRepository _repository;

        public Handler(IMapper mapper, IFireProgressionTableRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <inheritdoc />
        public async Task<PaginatedResponse<FireProgressionTableSummaryDto>> Handle(
            GetTablesQuery request,
            CancellationToken cancellationToken)
        {
            IEnumerable<FireProgressionTable> tables = await _repository.ListAsync(request, cancellationToken);
            int total = await _repository.CountAsync(cancellationToken);

            PaginatedResponse<FireProgressionTableSummaryDto> result = new()
            {
                Results = _mapper.Map<IEnumerable<FireProgressionTableSummaryDto>>(tables),
                Total = total,
            };

            return result;
        }
    }
}
