namespace Firestone.Application.FireTable.Commands;

using Contracts;
using Domain.Models;
using Services;

/// <summary>
/// A command to create a new FIRE table.
/// </summary>
public class AddFireTableCommand : IRequest<FireTableDto>
{
    /// <summary>
    /// The expected yearly inflation rate.
    /// </summary>
    public double YearlyInflationRate { get; init; }

    /// <summary>
    /// The expected yearly nominal return rate.
    /// </summary>
    public double YearlyNominalReturnRate { get; init; }

    /// <summary>
    /// The target asset value at retirement.
    /// </summary>
    public double RetirementTarget { get; init; }

    /// <summary>
    /// The number of years to reach the target asset value.
    /// </summary>
    public int YearsUntilRetirement { get; init; }

    public class Validator : AbstractValidator<AddFireTableCommand>
    {
        public Validator()
        {
            RuleFor(x => x.YearsUntilRetirement).GreaterThan(0);
            RuleFor(x => x.RetirementTarget).GreaterThan(0);
            RuleFor(x => x.YearlyInflationRate).GreaterThan(0);
            RuleFor(x => x.YearlyNominalReturnRate).GreaterThan(0);
        }
    }

    public class Handler : IRequestHandler<AddFireTableCommand, FireTableDto>
    {
        private readonly IFireTableRepository _fireTableRepository;
        private readonly IMapper _mapper;

        public Handler(IMapper mapper, IFireTableRepository fireTableRepository)
        {
            _mapper = mapper;
            _fireTableRepository = fireTableRepository;
        }

        /// <inheritdoc />
        public async Task<FireTableDto> Handle(AddFireTableCommand request, CancellationToken cancellationToken)
        {
            FireTable table = await _fireTableRepository.AddAsync(
                request.YearlyInflationRate,
                request.YearlyNominalReturnRate,
                request.RetirementTarget,
                request.YearsUntilRetirement,
                cancellationToken);

            return _mapper.Map<FireTableDto>(table);
        }
    }
}
