namespace Firestone.Domain.Data;

using System.ComponentModel;

public class PlannedIndividualContributionConfiguration : EntityBase
{
    [Description("The monthly income of the asset holder (after tax).")]
    public double MonthlyIncome { get; set; }

    [Description("The amount of planned monthly contributions towards the FIRE target.")]
    public double MonthlyContribution { get; set; }

    public virtual AssetHolder AssetHolder { get; set; } = default!;
}
