namespace Firestone.Application.Common.Contracts;

using Domain.Models;
using Waystone.Common.Application.Contracts.Mappings;

public class IndividualAssetsTotalDto : IMapFrom<IndividualAssetsTotalModel>
{
    public Guid Id { get; set; }

    public Guid AssetHolderId { get; set; }

    public Guid TableEntryId { get; set; }

    public double Value { get; set; }
}
