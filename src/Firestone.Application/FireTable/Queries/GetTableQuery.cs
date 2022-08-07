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

            var result = _mapper.Map<FireTableDto>(table);

            return result;
        }
    }
}
