namespace Firestone.Application.Common.Contracts;

using Domain.Enumerations;
using Domain.Models;
using Waystone.Common.Application.Contracts.Mappings;

public class ProjectedAssetsTotalDto : IMapFrom<ProjectedAssetsTotalModel>
{
    public Guid Id { get; set; }

    public Guid TableEntryId { get; set; }

    public ProjectionType ProjectionType { get; set; }

    public double? Value { get; set; }
}
