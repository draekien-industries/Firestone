namespace Firestone.Application.Common.Contracts;

using Domain.Models;
using Waystone.Common.Application.Contracts.Mappings;

public class RetirementTargetDto : IMapFrom<RetirementTargetModel>
{
    public Guid Id { get; set; }

    public Guid TableId { get; set; }

    public int YearsUntilRetirement { get; set; }

    public int MonthsUntilRetirement { get; set; }

    public double TargetValue { get; set; }

    public double TargetValueAtRetirement { get; set; }

    public double MinimumMonthlyGrowthRate { get; set; }

    public double MinimumMonthlyContributionValue { get; set; }

    public double CoastTargetStartingValue { get; set; }
}
