namespace Firestone.Application.Common.Contracts;

using Domain.Data;
using Waystone.Common.Application.Contracts.Mappings;

public class IndividualAssetsTotalDto : IMapFrom<IndividualAssetsTotal>
{
    public Guid Id { get; set; }

    public Guid AssetHolderId { get; set; }

    public Guid TableEntryId { get; set; }

    public double Value { get; set; }
}
