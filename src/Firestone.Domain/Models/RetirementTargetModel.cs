namespace Firestone.Domain.Models;

using Constants;
using Data;

public class RetirementTargetModel : EntityDomainModel<RetirementTargetConfiguration>
{
    public RetirementTargetModel(
        Guid tableId,
        int yearsUntilRetirement,
        double targetValue,
        InflationRateModel inflationRate,
        NominalReturnRateModel nominalReturnRate)
    {
        TableId = tableId;
        YearsUntilRetirement = yearsUntilRetirement;
        TargetValue = targetValue;
        TargetValueAtRetirement = CalculateTargetValueAtRetirement(inflationRate);
        CoastTargetStartingValue = CalculateCoastTargetStartingValue(nominalReturnRate);
    }

    public RetirementTargetModel(
        RetirementTargetConfiguration entity) : base(entity.Id)
    {
        TableId = entity.TableId;
        YearsUntilRetirement = entity.YearsUntilRetirement;
        TargetValue = entity.TargetValue;
        TargetValueAtRetirement = entity.TargetValueAtRetirement;
        MinimMonthlyGrowthRate = entity.MinimumMonthlyGrowthRate;
        MinimumMonthlyContributionValue = entity.MinimumMonthlyContributionValue;
        CoastTargetStartingValue = entity.CoastTargetStartingValue;
    }

    public Guid TableId { get; }

    public int YearsUntilRetirement { get; }

    public int MonthsUntilRetirement => YearsUntilRetirement * FirestoneValues.MonthsPerYear;

    public double TargetValue { get; }

    public double TargetValueAtRetirement { get; }

    public double? MinimMonthlyGrowthRate { get; private set; }

    public double? MinimumMonthlyContributionValue { get; private set; }

    public double CoastTargetStartingValue { get; }

    /// <inheritdoc />
    protected override RetirementTargetConfiguration CreateEntity()
    {
        return new RetirementTargetConfiguration
        {
            TableId = TableId,
            YearsUntilRetirement = YearsUntilRetirement,
            TargetValue = TargetValue,
            TargetValueAtRetirement = TargetValueAtRetirement,
            MinimumMonthlyGrowthRate = MinimMonthlyGrowthRate,
            MinimumMonthlyContributionValue = MinimumMonthlyContributionValue,
            CoastTargetStartingValue = CoastTargetStartingValue,
        };
    }

    public void SetMinimumMonthlyGrowthRate(FireProgressionTableEntryModel initialInvestment)
    {
        if (!initialInvestment.TotalAssetValues.HasValue)
        {
            throw new InvalidOperationException("Initial investment must have a total asset value");
        }

        MinimMonthlyGrowthRate = Math.Pow(
                                     TargetValueAtRetirement / initialInvestment.TotalAssetValues.Value,
                                     1.0 / MonthsUntilRetirement)
                               - 1;
    }

    public void SetMinimumMonthlyContributionValue(
        FireProgressionTableEntryModel initialInvestment,
        NominalReturnRateModel nominalReturnRate)
    {
        if (!initialInvestment.TotalAssetValues.HasValue)
        {
            throw new InvalidOperationException("Initial investment must have a total asset value");
        }

        MinimumMonthlyContributionValue = nominalReturnRate.MonthlyReturnRate
                                        * (TargetValueAtRetirement
                                         - initialInvestment.TotalAssetValues.Value
                                         * Math.Pow(1 + nominalReturnRate.MonthlyReturnRate, MonthsUntilRetirement))
                                        / (Math.Pow(1 + nominalReturnRate.MonthlyReturnRate, MonthsUntilRetirement)
                                         - 1);
    }

    private double CalculateTargetValueAtRetirement(InflationRateModel inflationRate)
    {
        return TargetValue * Math.Pow(1.0 + inflationRate.MonthlyRate, YearsUntilRetirement);
    }

    private double CalculateCoastTargetStartingValue(NominalReturnRateModel nominalReturnRate)
    {
        return TargetValueAtRetirement
             / Math.Pow(1.0 + nominalReturnRate.MonthlyReturnRate, MonthsUntilRetirement);
    }
}
