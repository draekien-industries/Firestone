namespace Firestone.Domain.Data;

using System.ComponentModel.DataAnnotations.Schema;

public class FireProgressionTable : EntityBase
{
    [ForeignKey(nameof(InflationRateConfiguration))]
    public Guid? InflationRateConfigurationId { get; set; }

    [ForeignKey(nameof(NominalReturnRateConfiguration))]
    public Guid? NominalReturnRateConfigurationId { get; set; }

    [ForeignKey(nameof(RetirementTargetConfiguration))]
    public Guid? RetirementTargetConfigurationId { get; set; }

    public virtual InflationRateConfiguration? InflationRateConfiguration { get; set; }

    public virtual NominalReturnRateConfiguration? NominalReturnRateConfiguration { get; set; }

    public virtual RetirementTargetConfiguration? RetirementTargetConfiguration { get; set; }

    public virtual ICollection<FireProgressionTableEntry> FireProgressionTableEntries { get; } =
        new List<FireProgressionTableEntry>();

    public virtual ICollection<AssetHolder> AssetHolders { get; set; } = new List<AssetHolder>();
}
