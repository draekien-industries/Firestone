namespace Firestone.Domain.Models;

public class AssetHolder : DbEntity
{
    public Guid FireTableId { get; set; }

    public string Name { get; set; } = default!;

    public double ExpectedMonthlyIncome { get; set; }

    public double PlannedMonthlyContribution { get; set; }


    public FireTable FireTable { get; set; } = default!;

    public ICollection<Assets> Assets { get; set; } = new List<Assets>();

    public static AssetHolder Initialise(
        Guid fireTableId,
        string name,
        double expectedMonthlyIncome,
        double plannedMonthlyContribution)
    {
        return new AssetHolder
        {
            FireTableId = fireTableId,
            Name = name,
            ExpectedMonthlyIncome = expectedMonthlyIncome,
            PlannedMonthlyContribution = plannedMonthlyContribution,
        };
    }
}
