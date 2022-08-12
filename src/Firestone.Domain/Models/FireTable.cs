namespace Firestone.Domain.Models;

using Constants;

public class FireTable : DbEntity
{
    public string Name { get; set; } = string.Empty;

    public double YearlyInflationRate { get; set; }

    public double MonthlyInflationRate => YearlyInflationRate / FirestoneValues.MonthsPerYear;

    public double YearlyNominalReturnRate { get; set; }

    public double MonthlyNominalReturnRate => YearlyNominalReturnRate / FirestoneValues.MonthsPerYear;

    public double RetirementTargetBeforeInflation { get; set; }

    public int YearsToRetirement { get; set; }

    public int MonthsToRetirement => YearsToRetirement * FirestoneValues.MonthsPerYear;

    public double CombinedMonthlyContribution =>
        AssetHolders.Sum(assetHolder => assetHolder.PlannedMonthlyContribution);

    public ICollection<LineItem> LineItems { get; set; } = new List<LineItem>();

    public ICollection<AssetHolder> AssetHolders { get; set; } = new List<AssetHolder>();

    public static FireTable Initialise(
        string name,
        double yearlyInflationRate,
        double yearlyNominalReturnRate,
        double retirementTargetBeforeInflation,
        int yearsToRetirement)
    {
        return new FireTable
        {
            Name = name,
            YearlyInflationRate = yearlyInflationRate,
            YearlyNominalReturnRate = yearlyNominalReturnRate,
            RetirementTargetBeforeInflation = retirementTargetBeforeInflation,
            YearsToRetirement = yearsToRetirement,
        };
    }
}
