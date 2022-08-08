namespace Firestone.Application.FireGraph.Contracts;

using Domain.Models;
using Waystone.Common.Application.Contracts.Mappings;

public class FireGraphDto : IMapFrom<FireGraph>
{
    public Guid Id { get; set; }

    public double CoastTargetStartingBalance { get; set; }

    public double MinimumMonthlyContribution { get; set; }

    public IEnumerable<DataPointDto> RecordedAssets { get; set; } = new List<DataPointDto>();

    public IEnumerable<DataPointDto> ProjectedAssets { get; set; } = new List<DataPointDto>();

    public AdjustedTargetsDto AdjustedTargets { get; set; }
}
