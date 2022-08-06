namespace Firestone.Application.FireProgressionTable.Queries;

using AutoMapper;
using Common.Contracts;
using Common.Repositories;
using Domain.Data;
using MediatR;

public class GetTableByIdQuery : IRequest<FireProgressionTableDto>
{
    public Guid Id { get; init; }

    public class Handler : IRequestHandler<GetTableByIdQuery, FireProgressionTableDto>
    {
        private readonly IMapper _mapper;
        private readonly IFireProgressionTableRepository _repository;

        public Handler(IFireProgressionTableRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<FireProgressionTableDto> Handle(
            GetTableByIdQuery request,
            CancellationToken cancellationToken)
        {
            FireProgressionTable table = await _repository.GetAsync(request.Id, cancellationToken);

            return _mapper.Map<FireProgressionTableDto>(table);
        }
    }
}
