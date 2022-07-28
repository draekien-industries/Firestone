namespace Firestone.Application.FireProgressionTable.Commands;

using AutoMapper;
using Common.Contracts;
using Common.Data;
using Domain.Data;
using Domain.Models;
using FluentValidation;
using MediatR;
using Repositories;

public class PopulateTableCommand : IRequest<FireProgressionTableDto>
{
    public Guid TableId { get; init; }

    public class Validator : AbstractValidator<PopulateTableCommand>
    {
        public Validator()
        {
            RuleFor(x => x.TableId).NotNull();
        }
    }

    public class Handler : IRequestHandler<PopulateTableCommand, FireProgressionTableDto>
    {
        private readonly IFirestoneDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFireProgressionTableRepository _repository;

        public Handler(IFirestoneDbContext context, IFireProgressionTableRepository repository, IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<FireProgressionTableDto> Handle(
            PopulateTableCommand request,
            CancellationToken cancellationToken)
        {
            FireProgressionTable entity = await _repository.GetAsync(request.TableId, cancellationToken);

            FireProgressionTableModel table = new(entity);

            table.EnsureTableCanBePopulated();

            RetirementTargetModel retirementTarget = table.RetirementTarget!;
            InflationRateModel inflationRate = table.InflationRate!;
            NominalReturnRateModel nominalReturnRate = table.NominalReturnRate!;
            FireProgressionTableEntryModel previous = table.Entries.First();

            List<FireProgressionTableEntryModel> newEntries = new();

            for (var i = 1; i < retirementTarget.MonthsUntilRetirement; i++)
            {
                DateTime date = previous.DateTime.AddMonths(1);

                FireProgressionTableEntryModel newEntry = new(
                    entity.Id,
                    date,
                    retirementTarget,
                    inflationRate,
                    nominalReturnRate,
                    previous);

                newEntries.Add(newEntry);
            }

            List<FireProgressionTableEntry> newEntities = newEntries.Select(x => x.ToEntity()).ToList();

            await _context.FireProgressionTableEntries.AddRangeAsync(newEntities, cancellationToken);
            await _context.SaveChangeAsync(cancellationToken);

            entity = await _repository.GetAsync(request.TableId, cancellationToken);

            table = new FireProgressionTableModel(entity);

            return _mapper.Map<FireProgressionTableDto>(table);
        }
    }
}
