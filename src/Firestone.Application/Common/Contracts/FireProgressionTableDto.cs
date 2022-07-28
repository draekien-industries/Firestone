namespace Firestone.Application.Common.Contracts;

using Domain.Models;
using Waystone.Common.Application.Contracts.Mappings;

public class FireProgressionTableDto : IMapFrom<FireProgressionTableModel>
{
    public Guid Id { get; set; }

    public InflationRateDto? InflationRate { get; set; }

    public NominalReturnRateDto? NominalReturnRate { get; set; }

    public RetirementTargetDto? RetirementTarget { get; set; }

    public IEnumerable<AssetHolderDto> AssetHolders { get; set; } = new List<AssetHolderDto>();

    public IEnumerable<FireProgressionTableEntryDto> Entries { get; set; } = new List<FireProgressionTableEntryDto>();
}
