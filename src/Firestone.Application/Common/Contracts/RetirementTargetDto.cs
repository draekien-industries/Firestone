namespace Firestone.Application.Common.Contracts;

using Domain.Data;
using Waystone.Common.Application.Contracts.Mappings;

public class RetirementTargetDto : IMapFrom<RetirementTargetConfiguration>
{
    public Guid Id { get; set; }

    public Guid TableId { get; set; }

    public int YearsUntilRetirement { get; set; }

    public int MonthsUntilRetirement { get; set; }

    public double TargetValue { get; set; }
}
