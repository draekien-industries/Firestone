namespace Firestone.Domain.Data;

using System.ComponentModel.DataAnnotations.Schema;

public class FireProgressionTable : Entity
{
    private FireProgressionTable()
    { }

    public FireProgressionTable(
        double yearlyInflationRate,
        double yearlyNominalReturnRate,
        int yearsUntilRetirement,
        double retirementTargetValue)
    {
        InflationRateConfiguration = ConfigureInflationRate(yearlyInflationRate);
        NominalReturnRateConfiguration = ConfigureNominalReturnRate(yearlyNominalReturnRate);
        RetirementTargetConfiguration = ConfigureRetirementTarget(yearsUntilRetirement, retirementTargetValue);
    }

    [NotMapped]
    public double? MinimumMonthlyGrowthRate => GetMinimumMonthlyGrowthRate();

    [NotMapped]
    public double? MinimumMonthlyContributionValue => GetMinimumMonthlyContributionValue();

    [NotMapped]
    public double? InitialCoastTarget => GetInitialCoastTarget();

    [NotMapped]
    public double? TargetValueAtPointOfRetirement => GetTargetValueAtPointOfRetirement();

    public virtual InflationRateConfiguration InflationRateConfiguration { get; set; }

    public virtual NominalReturnRateConfiguration NominalReturnRateConfiguration { get; set; }

    public virtual RetirementTargetConfiguration RetirementTargetConfiguration { get; set; }

    public virtual ICollection<FireProgressionTableEntry> Entries { get; set; } =
        new List<FireProgressionTableEntry>();

    public virtual ICollection<AssetHolder> AssetHolders { get; set; } = new List<AssetHolder>();

    private InflationRateConfiguration ConfigureInflationRate(double yearlyRate)
    {
        return new InflationRateConfiguration(Id, yearlyRate);
    }

    private NominalReturnRateConfiguration ConfigureNominalReturnRate(double yearlyReturnRate)
    {
        return new NominalReturnRateConfiguration(Id, yearlyReturnRate);
    }

    private RetirementTargetConfiguration ConfigureRetirementTarget(int yearsUntilRetirement, double targetValue)
    {
        return new RetirementTargetConfiguration(Id, yearsUntilRetirement, targetValue);
    }

    private double? GetMinimumMonthlyGrowthRate()
    {
        if (!Entries.Any()) return default;
        if (!TargetValueAtPointOfRetirement.HasValue) return default;

        FireProgressionTableEntry initialInvestment = Entries.First();

        if (!initialInvestment.TotalAssetValues.HasValue)
        {
            throw new InvalidOperationException("Initial investment total asset value is null");
        }

        double targetValueAtRetirement = TargetValueAtPointOfRetirement.Value;
        int monthsUntilRetirement = RetirementTargetConfiguration.MonthsUntilRetirement;
        double totalAssetValue = initialInvestment.TotalAssetValues.Value;

        return Math.Pow(targetValueAtRetirement / totalAssetValue, 1.0 / monthsUntilRetirement);
    }

    private double? GetMinimumMonthlyContributionValue()
    {
        if (!Entries.Any())
        {
            return default;
        }

        if (!TargetValueAtPointOfRetirement.HasValue) return default;

        FireProgressionTableEntry initialInvestment = Entries.First();

        if (!initialInvestment.TotalAssetValues.HasValue)
        {
            throw new InvalidOperationException("Initial investment total asset value is null");
        }

        double monthlyReturnRate = NominalReturnRateConfiguration.MonthlyReturnRate;
        int monthsUntilRetirement = RetirementTargetConfiguration.MonthsUntilRetirement;
        double targetValueAtRetirement = TargetValueAtPointOfRetirement.Value;

        double scaledReturnRate = Math.Pow(1.0 + monthlyReturnRate, monthsUntilRetirement);

        return monthlyReturnRate
             * (targetValueAtRetirement - initialInvestment.TotalAssetValues.Value * scaledReturnRate)
             / (scaledReturnRate - 1.0);
    }

    private double? GetInitialCoastTarget()
    {
        if (!TargetValueAtPointOfRetirement.HasValue) return default;

        double targetValueAtRetirement = TargetValueAtPointOfRetirement.Value;
        int monthsUntilRetirement = RetirementTargetConfiguration.MonthsUntilRetirement;
        double monthlyReturnRate = NominalReturnRateConfiguration.MonthlyReturnRate;

        return targetValueAtRetirement / Math.Pow(1.0 + monthlyReturnRate, monthsUntilRetirement);
    }

    private double? GetTargetValueAtPointOfRetirement()
    {
        double targetValue = RetirementTargetConfiguration.TargetValue;
        double monthlyInflationRate = InflationRateConfiguration.MonthlyRate;
        int yearsUntilRetirement = RetirementTargetConfiguration.YearsUntilRetirement;

        return targetValue * Math.Pow(1.0 + monthlyInflationRate, yearsUntilRetirement);
    }
}
