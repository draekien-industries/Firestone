namespace Firestone.Domain.Data;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

public class AssetHolder : Entity
{
    private AssetHolder()
    { }

    public AssetHolder(Guid tableId, string name, double monthlyIncome, double monthlyContribution)
    {
        Name = name;
        TableId = tableId;
        PlannedIndividualContributionConfiguration =
            ConfigurePlannedIndividualContribution(monthlyIncome, monthlyContribution);
    }

    [Description("The name of the individual or organization that holds assets.")]
    public string Name { get; set; } = default!;

    [ForeignKey(nameof(Table))]
    public Guid TableId { get; set; }

    public virtual PlannedIndividualContributionConfiguration PlannedIndividualContributionConfiguration { get; set; }

    public virtual FireProgressionTable Table { get; set; } = default!;

    public virtual ICollection<IndividualAssetsTotal> IndividualAssetValues { get; set; } =
        new List<IndividualAssetsTotal>();

    private PlannedIndividualContributionConfiguration ConfigurePlannedIndividualContribution(
        double monthlyIncome,
        double monthlyContribution)
    {
        return new PlannedIndividualContributionConfiguration(Id, monthlyIncome, monthlyContribution);
    }
}
