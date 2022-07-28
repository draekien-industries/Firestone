namespace Firestone.Application.Common.Contracts;

using Domain.Models;
using Waystone.Common.Application.Contracts.Mappings;

public class AssetHolderDto : IMapFrom<AssetHolderModel>
{
    public Guid Id { get; set; }

    public Guid TableId { get; set; }

    public string Name { get; set; } = default!;

    public PlannedIndividualContributionDto PlannedIndividualContribution { get; set; } = default!;

    public IEnumerable<IndividualAssetsTotalDto> IndividualAssetsTotals { get; set; } = default!;
}
