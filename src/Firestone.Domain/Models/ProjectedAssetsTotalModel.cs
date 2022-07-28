namespace Firestone.Domain.Models;

using Data;
using Enumerations;

public class ProjectedAssetsTotalModel : EntityDomainModel<ProjectedAssetsTotal>
{
    public ProjectedAssetsTotalModel(Guid tableEntryId, ProjectionType projectionType, double? value)
    {
        TableEntryId = tableEntryId;
        ProjectionType = projectionType;
        Value = value;
    }

    public ProjectedAssetsTotalModel(ProjectedAssetsTotal entity) : base(entity.Id)
    {
        TableEntryId = entity.TableEntryId;
        ProjectionType = entity.ProjectionType;
        Value = entity.Value;
    }

    public Guid TableEntryId { get; }

    public ProjectionType ProjectionType { get; }

    public double? Value { get; }

    /// <inheritdoc />
    protected override ProjectedAssetsTotal CreateEntity()
    {
        return new ProjectedAssetsTotal
        {
            TableEntryId = TableEntryId,
            ProjectionType = ProjectionType,
            Value = Value,
        };
    }
}
