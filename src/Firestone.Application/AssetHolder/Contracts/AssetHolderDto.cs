namespace Firestone.Application.AssetHolder.Contracts;

using Assets.Contracts;
using Domain.Models;
using Waystone.Common.Application.Contracts.Mappings;

/// <summary>
/// Data transfer object for an asset holder.
/// </summary>
public class AssetHolderDto : IMapFrom<AssetHolder>
{
    /// <summary>
    /// The ID of the asset holder.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The ID of the FIRE table that this asset holder is associated with.
    /// </summary>
    public Guid FireTableId { get; set; }

    /// <summary>
    /// The name of the asset holder.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// The expected monthly income of the asset holder.
    /// </summary>
    public double ExpectedMonthlyIncome { get; set; }

    /// <summary>
    /// The planned monthly contribution (towards growing assets) for the asset holder.
    /// </summary>
    public double PlannedMonthlyContribution { get; set; }

    /// <summary>
    /// The historical records of the asset holder's total asset values.
    /// </summary>
    public IEnumerable<AssetsDto> Assets { get; set; } = Array.Empty<AssetsDto>();
}
