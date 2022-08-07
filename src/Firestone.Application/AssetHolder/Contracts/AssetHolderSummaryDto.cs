namespace Firestone.Application.AssetHolder.Contracts;

using Domain.Models;
using Waystone.Common.Application.Contracts.Mappings;

/// <summary>
/// Data transfer object for an asset holder summary details.
/// </summary>
public class AssetHolderSummaryDto : IMapFrom<AssetHolder>
{
    /// <summary>
    /// The ID of the asset holder.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the asset holder.
    /// </summary>
    public string Name { get; set; } = default!;
}
