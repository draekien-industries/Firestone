namespace Firestone.Domain.Data;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

[Description("An asset holder's total asset value on the date of the table entry.")]
public class IndividualAssetsTotal : EntityBase
{
    [ForeignKey(nameof(AssetHolder))]
    [Description("The asset holder for which the asset value is recorded.")]
    public Guid AssetHolderId { get; set; }

    [ForeignKey(nameof(FireProgressionTableEntry))]
    [Description("The table entry for which the asset value is recorded.")]
    public Guid TableEntryId { get; set; }

    [Description("The asset value.")]
    public double Value { get; set; }

    public virtual AssetHolder AssetHolder { get; set; } = default!;

    public virtual FireProgressionTableEntry FireProgressionTableEntry { get; set; } = default!;
}
