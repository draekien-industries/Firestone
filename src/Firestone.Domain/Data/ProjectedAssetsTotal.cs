namespace Firestone.Domain.Data;

using System.ComponentModel.DataAnnotations.Schema;
using Enumerations;

public class ProjectedAssetsTotal : Entity
{
    private ProjectedAssetsTotal()
    { }

    public ProjectedAssetsTotal(Guid tableEntryId, ProjectionType projectionType, double value)
    {
        TableEntryId = tableEntryId;
        ProjectionType = projectionType;
        Value = value;
    }

    [ForeignKey(nameof(FireProgressionTableEntry))]
    public Guid TableEntryId { get; set; }

    public ProjectionType ProjectionType { get; set; }

    public double? Value { get; set; }

    public virtual FireProgressionTableEntry FireProgressionTableEntry { get; set; } = default!;
}
