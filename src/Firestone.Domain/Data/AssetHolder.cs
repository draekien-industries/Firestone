namespace Firestone.Domain.Data;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

public class AssetHolder : EntityBase
{
    [Description("The name of the individual or organization that holds assets.")]
    public string Name { get; set; } = default!;

    [ForeignKey(nameof(PlannedIndividualContributionConfiguration))]
    public Guid PlannedIndividualContributionConfigurationId { get; set; }

    [ForeignKey(nameof(Table))]
    public Guid TableId { get; set; }

    public virtual PlannedIndividualContributionConfiguration PlannedIndividualContributionConfiguration { get; set; } =
        default!;

    public virtual FireProgressionTable Table { get; set; } = default!;

    public virtual ICollection<IndividualAssetsTotal> IndividualAssetValues { get; set; } =
        new List<IndividualAssetsTotal>();
}
