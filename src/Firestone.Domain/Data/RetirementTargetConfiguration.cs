namespace Firestone.Domain.Data;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Constants;

public class RetirementTargetConfiguration : EntityBase
{
    [Description("The number of years (from the initial investment) that you wish to retire in.")]
    public int YearsUntilRetirement { get; set; }

    [NotMapped]
    [Description("The number of months (from the initial investment) that you wish to retire in.")]
    public int MonthsUntilRetirement => YearsUntilRetirement * FirestoneValues.MonthsPerYear;

    [Description("The amount of money you will need before retiring (at the point of initial investment).")]
    public double TargetValue { get; set; }

    [Description("The amount of money you will need before retiring (adjusted for inflation at point of retirement).")]
    public double TargetValueAtRetirement { get; set; }

    [Description("The rate at which your assets will need to grow (in % decimals) to meet your target value.")]
    public double? MinimumMonthlyGrowthRate { get; set; }

    [Description("The total amount of money you will need to invest into assets per month (adjusted for inflation).")]
    public double? MinimumMonthlyContributionValue { get; set; }

    [Description("The point at which you no longer need to put aside money to meet your target value.")]
    public double CoastTargetStartingValue { get; set; }

    [ForeignKey(nameof(FireProgressionTable))]
    public Guid TableId { get; set; }

    public virtual FireProgressionTable FireProgressionTable { get; set; } = default!;
}
