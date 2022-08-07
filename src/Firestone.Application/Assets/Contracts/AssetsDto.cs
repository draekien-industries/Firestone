namespace Firestone.Application.Assets.Contracts;

using Domain.Models;
using Waystone.Common.Application.Contracts.Mappings;

/// <summary>
/// Data transfer object for an asset holder's assets.
/// </summary>
public class AssetsDto : IMapFrom<Assets>
{
    /// <summary>
    /// The ID of the assets.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The ID of the asset holder.
    /// </summary>
    public Guid AssetHolderId { get; set; }

    /// <summary>
    /// The ID of the line item the assets where recorded against.
    /// </summary>
    public Guid LineItemId { get; set; }

    /// <summary>
    /// The assets total amount.
    /// </summary>
    public double Amount { get; set; }
}
