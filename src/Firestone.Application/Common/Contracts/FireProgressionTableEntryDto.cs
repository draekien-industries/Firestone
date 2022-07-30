namespace Firestone.Application.Common.Contracts;

using Domain.Data;
using Waystone.Common.Application.Contracts.Mappings;

public class FireProgressionTableEntryDto : IMapFrom<FireProgressionTableEntry>
{
    public Guid Id { get; set; }

    public Guid TableId { get; set; }

    public Guid? PreviousTableEntryId { get; set; }

    public DateOnly Date { get; set; }

    public double? RetirementTargetValue { get; set; }

    public double? CoastTargetValue { get; set; }

    public double? MinimumGrowthTargetValue { get; set; }

    public double? TotalAssetValues { get; set; }

    public double? ChangeInTotalAssetValues { get; set; }

    public IEnumerable<IndividualAssetsTotalDto> IndividualAssetValues { get; set; } =
        new List<IndividualAssetsTotalDto>();

    public IEnumerable<ProjectedAssetsTotalDto> ProjectedTotalAssetValues { get; set; } =
        new List<ProjectedAssetsTotalDto>();
}
