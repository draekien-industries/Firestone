namespace Firestone.Application.Common.Contracts;

using Domain.Data;
using Waystone.Common.Application.Contracts.Mappings;

public class FireProgressionTableDto : IMapFrom<FireProgressionTable>
{
    public Guid Id { get; set; }

    public double? MinimumMonthlyGrowthRate { get; set; }
    public double? MinimumMonthlyContributionValue { get; set; }
    public double? InitialCoastTarget { get; set; }
    public double? TargetValueAtPointOfRetirement { get; set; }

    public InflationRateDto InflationRateConfiguration { get; set; } = default!;

    public NominalReturnRateDto NominalReturnRateConfiguration { get; set; } = default!;

    public RetirementTargetDto RetirementTargetConfiguration { get; set; } = default!;

    public IEnumerable<AssetHolderDto> AssetHolders { get; set; } = new List<AssetHolderDto>();

    public IEnumerable<FireProgressionTableEntryDto> Entries { get; set; } = new List<FireProgressionTableEntryDto>();
}
