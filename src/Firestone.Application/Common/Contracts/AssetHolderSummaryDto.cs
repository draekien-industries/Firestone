namespace Firestone.Application.Common.Contracts;

using Domain.Data;
using Waystone.Common.Application.Contracts.Mappings;

public class AssetHolderSummaryDto : IMapFrom<AssetHolder>
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;
}
