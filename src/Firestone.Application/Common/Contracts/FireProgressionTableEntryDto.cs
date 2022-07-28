namespace Firestone.Application.Common.Contracts;

using Domain.Models;
using Waystone.Common.Application.Contracts.Mappings;

public class FireProgressionTableEntryDto : IMapFrom<FireProgressionTableEntryModel>
{
    public Guid Id { get; set; }

    public Guid TableId { get; set; }

    public Guid? PreviousTableEntryId { get; set; }

    public DateOnly Date { get; set; }

    public double? RetirementTargetValueSnapshot { get; set; }

    public double? CoastTargetValueSnapshot { get; set; }

    public double? MinimumGrowthTargetValueSnapshot { get; set; }

    public double? TotalAssetValues { get; set; }

    public double? ChangeInTotalAssetValues { get; set; }

    public IEnumerable<IndividualAssetsTotalDto> IndividualAssetsTotals { get; set; } =
        new List<IndividualAssetsTotalDto>();

    public IEnumerable<ProjectedAssetsTotalDto> ProjectedAssetsTotals { get; set; } =
        new List<ProjectedAssetsTotalDto>();
}
