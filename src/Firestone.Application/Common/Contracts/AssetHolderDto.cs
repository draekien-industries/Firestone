namespace Firestone.Application.Common.Contracts;

using Domain.Data;
using Waystone.Common.Application.Contracts.Mappings;

public class AssetHolderDto : IMapFrom<AssetHolder>
{
    public Guid Id { get; set; }

    public Guid TableId { get; set; }

    public string Name { get; set; } = default!;

    public PlannedIndividualContributionDto PlannedIndividualContributionConfiguration { get; set; } = default!;

    public IEnumerable<IndividualAssetsTotalDto> IndividualAssetValues { get; set; } = default!;
}
