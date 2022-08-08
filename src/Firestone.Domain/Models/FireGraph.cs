namespace Firestone.Domain.Models;

public class FireGraph
{
    public Guid Id { get; set; }

    public double CoastTargetStartingBalance { get; set; }

    public double MinimumMonthlyContribution { get; set; }

    public IEnumerable<DataPoint> RecordedAssets { get; set; } = new List<DataPoint>();

    public IEnumerable<DataPoint> ProjectedAssets { get; set; } = new List<DataPoint>();

    public AdjustedTargets AdjustedTargets { get; set; }
}
