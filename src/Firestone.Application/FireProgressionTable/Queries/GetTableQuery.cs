﻿namespace Firestone.Application.FireProgressionTable.Queries;

using AutoMapper;
using Common.Contracts;
using Common.Repositories;
using Domain.Data;
using MediatR;

public class GetTableQuery : IRequest<FireProgressionTableDto>
{
    public Guid Id { get; init; }

    public class Handler : IRequestHandler<GetTableQuery, FireProgressionTableDto>
    {
        private readonly IMapper _mapper;
        private readonly IFireProgressionTableRepository _repository;

        public Handler(IFireProgressionTableRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<FireProgressionTableDto> Handle(GetTableQuery request, CancellationToken cancellationToken)
        {
            FireProgressionTable table = await _repository.GetAsync(request.Id, cancellationToken);

            return _mapper.Map<FireProgressionTableDto>(table);
        }
    }
}
