namespace Firestone.Application.FireTable.Contracts;

using AssetHolder.Contracts;
using Domain.Models;
using Waystone.Common.Application.Contracts.Mappings;

/// <summary>
/// A summary of a FIRE Table
/// </summary>
public class FireTableSummaryDto : IMapFrom<FireTable>
{
    /// <summary>
    /// The ID of the FIRE table.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The asset holders associated with the FIRE table.
    /// </summary>
    public IEnumerable<AssetHolderSummaryDto> AssetHolders { get; set; } = Array.Empty<AssetHolderSummaryDto>();
}
