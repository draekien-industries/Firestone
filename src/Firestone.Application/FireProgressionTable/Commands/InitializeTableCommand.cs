namespace Firestone.Application.FireProgressionTable.Commands;

using AutoMapper;
using Common.Contracts;
using Common.Data;
using Domain.Data;
using MediatR;
using Repositories;

public class InitializeTableCommand : IRequest<FireProgressionTableDto>
{
    public double YearlyInflationRate { get; init; }

    public double YearlyNominalReturnRate { get; init; }

    public int YearsUntilRetirement { get; init; }

    public double RetirementTarget { get; init; }

    public class Handler : IRequestHandler<InitializeTableCommand, FireProgressionTableDto>
    {
        private readonly IFirestoneDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFireProgressionTableRepository _repository;

        public Handler(IFirestoneDbContext context, IMapper mapper, IFireProgressionTableRepository repository)
        {
            _context = context;
            _mapper = mapper;
            _repository = repository;
        }

        /// <inheritdoc />
        public async Task<FireProgressionTableDto> Handle(
            InitializeTableCommand request,
            CancellationToken cancellationToken)
        {
            FireProgressionTable table = new(
                request.YearlyInflationRate,
                request.YearlyNominalReturnRate,
                request.YearsUntilRetirement,
                request.RetirementTarget);

            await _context.FireProgressionTables.AddAsync(table, cancellationToken);
            await _context.SaveChangeAsync(cancellationToken);

            return _mapper.Map<FireProgressionTableDto>(table);
        }
    }
}
