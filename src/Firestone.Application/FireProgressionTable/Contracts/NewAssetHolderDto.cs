namespace Firestone.Application.FireProgressionTable.Contracts;

public class NewAssetHolderDto
{
    public string Name { get; init; } = default!;

    public double MonthlyIncome { get; init; }

    public double PlannedMonthlyContribution { get; init; }
}
