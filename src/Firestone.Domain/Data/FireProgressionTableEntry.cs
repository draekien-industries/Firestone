namespace Firestone.Domain.Data;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

public class FireProgressionTableEntry : EntityBase
{
    [ForeignKey(nameof(FireProgressionTable))]
    [Description("The table this entry belongs to")]
    public Guid TableId { get; set; }

    [ForeignKey(nameof(Previous))]
    [Description("The previous entry in the table")]
    public Guid? PreviousTableEntryId { get; set; }

    [Description("The month the table entry corresponds to.")]
    public DateTime DateTime { get; set; }

    [NotMapped]
    [Description("The month the table entry corresponds to.")]
    public DateOnly Date => DateOnly.FromDateTime(DateTime);

    [Description("The retirement target value at the date of this entry.")]
    public double RetirementTargetValue { get; set; }

    [Description("The coast target value at the date of this entry.")]
    public double CoastTargetValue { get; set; }

    [Description("The minimum growth rate of total asset values at the date of this entry.")]
    public double? MinimumGrowthTargetValue { get; set; }

    [Description("The actual total asset values at the date of this entry.")]
    public double? TotalAssetValues { get; set; }

    [Description("The change in total asset values (compared to the previous entry).")]
    public double? ChangeInTotalAssetValues { get; set; }

    public virtual FireProgressionTable FireProgressionTable { get; set; } = default!;

    public virtual FireProgressionTableEntry? Previous { get; set; }

    public virtual ICollection<IndividualAssetsTotal> IndividualAssetValues { get; set; } =
        new List<IndividualAssetsTotal>();

    public virtual ICollection<ProjectedAssetsTotal> ProjectedTotalAssetValues { get; set; } =
        new List<ProjectedAssetsTotal>();
}
