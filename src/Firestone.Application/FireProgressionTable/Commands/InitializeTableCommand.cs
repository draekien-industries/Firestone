namespace Firestone.Application.FireProgressionTable.Commands;

using AutoMapper;
using Common.Contracts;
using Common.Repositories;
using Domain.Data;
using MediatR;

public class InitializeTableCommand : IRequest<FireProgressionTableDto>
{
    public double YearlyInflationRate { get; init; }

    public double YearlyNominalReturnRate { get; init; }

    public int YearsUntilRetirement { get; init; }

    public double RetirementTarget { get; init; }

    public class Handler : IRequestHandler<InitializeTableCommand, FireProgressionTableDto>
    {
        private readonly IMapper _mapper;
        private readonly IFireProgressionTableRepository _repository;

        public Handler(IMapper mapper, IFireProgressionTableRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <inheritdoc />
        public async Task<FireProgressionTableDto> Handle(
            InitializeTableCommand request,
            CancellationToken cancellationToken)
        {
            FireProgressionTable table = await _repository.AddAsync(request, cancellationToken);

            return _mapper.Map<FireProgressionTableDto>(table);
        }
    }
}
