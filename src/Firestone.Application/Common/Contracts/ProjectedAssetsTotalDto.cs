namespace Firestone.Application.Common.Contracts;

using Domain.Data;
using Domain.Enumerations;
using Waystone.Common.Application.Contracts.Mappings;

public class ProjectedAssetsTotalDto : IMapFrom<ProjectedAssetsTotal>
{
    public Guid Id { get; set; }

    public Guid TableEntryId { get; set; }

    public ProjectionType ProjectionType { get; set; }

    public double? Value { get; set; }
}
