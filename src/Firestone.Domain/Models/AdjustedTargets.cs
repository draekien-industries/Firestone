namespace Firestone.Domain.Models;

public class AdjustedTargets
{
    public IEnumerable<DataPoint> RetirementTargets { get; set; }

    public IEnumerable<DataPoint> CoastTargets { get; set; }

    public IEnumerable<DataPoint> MinimumGrowthTargets { get; set; }
}
