namespace Firestone.Application.FireGraph.Contracts;

using Domain.Models;
using Waystone.Common.Application.Contracts.Mappings;

public class AdjustedTargetsDto : IMapFrom<AdjustedTargets>
{
    public IEnumerable<DataPointDto> RetirementTargets { get; set; } = new List<DataPointDto>();

    public IEnumerable<DataPointDto> CoastTargets { get; set; } = new List<DataPointDto>();

    public IEnumerable<DataPointDto> MinimumGrowthTargets { get; set; } = new List<DataPointDto>();
}
