namespace Firestone.Application.FireProgressionTable.Commands;

using AutoMapper;
using Common.Contracts;
using Common.Data;
using Domain.Data;
using Domain.Models;
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
            FireProgressionTableModel table = new();

            FireProgressionTable entity = await _repository.AddAsync(table, cancellationToken);

            Guid tableId = entity.Id;

            InflationRateModel inflationRate = new(tableId, request.YearlyInflationRate);
            InflationRateConfiguration inflationRateEntity = inflationRate.ToEntity();
            await _context.InflationRates.AddAsync(inflationRateEntity, cancellationToken);

            NominalReturnRateModel nominalReturnRate = new(tableId, request.YearlyNominalReturnRate);
            NominalReturnRateConfiguration nominalReturnRateEntity = nominalReturnRate.ToEntity();
            await _context.NominalReturnRates.AddAsync(nominalReturnRateEntity, cancellationToken);

            RetirementTargetModel retirementTarget = new(
                tableId,
                request.YearsUntilRetirement,
                request.RetirementTarget,
                inflationRate,
                nominalReturnRate);

            RetirementTargetConfiguration retirementTargetEntity = retirementTarget.ToEntity();
            await _context.RetirementTargets.AddAsync(retirementTargetEntity, cancellationToken);

            await _context.SaveChangeAsync(cancellationToken);

            entity.InflationRateConfiguration = inflationRateEntity;
            entity.NominalReturnRateConfiguration = nominalReturnRateEntity;
            entity.RetirementTargetConfiguration = retirementTargetEntity;

            _context.FireProgressionTables.Update(entity);
            await _context.SaveChangeAsync(cancellationToken);

            entity = await _repository.GetAsync(entity.Id, cancellationToken);

            table = new FireProgressionTableModel(entity);

            return _mapper.Map<FireProgressionTableDto>(table);
        }
    }
}
