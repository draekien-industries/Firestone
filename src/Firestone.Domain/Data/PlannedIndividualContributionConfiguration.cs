namespace Firestone.Domain.Data;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

public class PlannedIndividualContributionConfiguration : Entity
{
    private PlannedIndividualContributionConfiguration()
    { }

    public PlannedIndividualContributionConfiguration(
        Guid assetHolderId,
        double monthlyIncome,
        double monthlyContribution)
    {
        AssetHolderId = assetHolderId;
        MonthlyIncome = monthlyIncome;
        MonthlyContribution = monthlyContribution;
    }

    [ForeignKey(nameof(AssetHolder))]
    public Guid AssetHolderId { get; set; }

    [Description("The monthly income of the asset holder (after tax).")]
    public double MonthlyIncome { get; set; }

    [Description("The amount of planned monthly contributions towards the FIRE target.")]
    public double MonthlyContribution { get; set; }

    public virtual AssetHolder AssetHolder { get; set; } = default!;
}
