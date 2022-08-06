namespace Firestone.Application.Common.Contracts;

using Domain.Data;
using Waystone.Common.Application.Contracts.Mappings;

public class FireProgressionTableSummaryDto : IMapFrom<FireProgressionTable>
{
    public Guid Id { get; set; }

    public IEnumerable<AssetHolderSummaryDto> AssetHolders { get; set; } = new List<AssetHolderSummaryDto>();
}
