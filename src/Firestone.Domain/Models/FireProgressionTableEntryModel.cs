namespace Firestone.Domain.Models;

using Data;
using Utils;

public class FireProgressionTableEntryModel : EntityDomainModel<FireProgressionTableEntry>
{
    public FireProgressionTableEntryModel(
        Guid tableId,
        DateTime dateTime,
        RetirementTargetModel retirementTarget)
    {
        TableId = tableId;
        DateTime = dateTime;
        RetirementTargetValueSnapshot = retirementTarget.TargetValue;
        CoastTargetValueSnapshot = retirementTarget.CoastTargetStartingValue;
        IndividualAssetsTotals = new List<IndividualAssetsTotalModel>();
        ProjectedAssetsTotals = new List<ProjectedAssetsTotalModel>();
    }

    public FireProgressionTableEntryModel(
        Guid tableId,
        DateTime dateTime,
        RetirementTargetModel retirementTarget,
        InflationRateModel inflationRate,
        NominalReturnRateModel nominalReturnRate,
        FireProgressionTableEntryModel previous)
    {
        TableId = tableId;
        DateTime = dateTime;
        RetirementTargetValueSnapshot = CalculateRetirementTargetValueSnapshot(previous, inflationRate);
        CoastTargetValueSnapshot = CalculateCoastTargetValueSnapshot(previous, nominalReturnRate);
        MinimumGrowthTargetValueSnapshot =
            CalculateMinimumGrowthTargetValueSnapshot(previous, nominalReturnRate, retirementTarget);
        IndividualAssetsTotals = new List<IndividualAssetsTotalModel>();
        ProjectedAssetsTotals = new List<ProjectedAssetsTotalModel>();
    }

    public FireProgressionTableEntryModel(FireProgressionTableEntry entity) : base(entity.Id)
    {
        TableId = entity.TableId;
        PreviousTableEntryId = entity.PreviousTableEntryId;
        DateTime = entity.DateTime;
        RetirementTargetValueSnapshot = entity.RetirementTargetValue;
        CoastTargetValueSnapshot = entity.CoastTargetValue;
        MinimumGrowthTargetValueSnapshot = entity.MinimumGrowthTargetValue;
        TotalAssetValues = entity.TotalAssetValues;
        ChangeInTotalAssetValues = entity.ChangeInTotalAssetValues;
        IndividualAssetsTotals = entity.IndividualAssetValues.Select(x => new IndividualAssetsTotalModel(x)).ToList();
        ProjectedAssetsTotals =
            entity.ProjectedTotalAssetValues.Select(x => new ProjectedAssetsTotalModel(x)).ToList();
    }

    public Guid TableId { get; }

    public Guid? PreviousTableEntryId { get; }

    public DateTime DateTime { get; }

    public DateOnly Date => DateOnly.FromDateTime(DateTime);

    public double RetirementTargetValueSnapshot { get; }

    public double CoastTargetValueSnapshot { get; }

    public double? MinimumGrowthTargetValueSnapshot { get; }

    public double? TotalAssetValues { get; private set; }

    public double? ChangeInTotalAssetValues { get; }

    public ICollection<IndividualAssetsTotalModel> IndividualAssetsTotals { get; }

    public ICollection<ProjectedAssetsTotalModel> ProjectedAssetsTotals { get; }

    private double? CalculateMinimumGrowthTargetValueSnapshot(
        FireProgressionTableEntryModel previous,
        NominalReturnRateModel nominalReturnRate,
        RetirementTargetModel retirementTarget)
    {
        return MathUtils.IncreaseByPercentage(
                   previous.MinimumGrowthTargetValueSnapshot!.Value,
                   nominalReturnRate.MonthlyReturnRate)
             + retirementTarget.MinimumMonthlyContributionValue;
    }

    private double CalculateCoastTargetValueSnapshot(
        FireProgressionTableEntryModel previous,
        NominalReturnRateModel nominalReturnRate)
    {
        return MathUtils.IncreaseByPercentage(previous.CoastTargetValueSnapshot, nominalReturnRate.MonthlyReturnRate);
    }

    public void AddIndividualAssets(IndividualAssetsTotalModel individualAssets)
    {
        IndividualAssetsTotals.Add(individualAssets);
        TotalAssetValues = CalculateTotalAssetValues(IndividualAssetsTotals);
    }

    private static double CalculateRetirementTargetValueSnapshot(
        FireProgressionTableEntryModel previous,
        InflationRateModel inflationRate)
    {
        return MathUtils.IncreaseByPercentage(previous.RetirementTargetValueSnapshot, inflationRate.MonthlyRate);
    }


    /// <inheritdoc />
    protected override FireProgressionTableEntry CreateEntity()
    {
        return new FireProgressionTableEntry
        {
            TableId = TableId,
            PreviousTableEntryId = PreviousTableEntryId,
            DateTime = DateTime,
            RetirementTargetValue = RetirementTargetValueSnapshot,
            CoastTargetValue = CoastTargetValueSnapshot,
            MinimumGrowthTargetValue = MinimumGrowthTargetValueSnapshot,
            TotalAssetValues = TotalAssetValues,
            ChangeInTotalAssetValues = ChangeInTotalAssetValues,
            IndividualAssetValues = IndividualAssetsTotals.Select(x => x.ToEntity()).ToList(),
            ProjectedTotalAssetValues = ProjectedAssetsTotals.Select(x => x.ToEntity()).ToList(),
        };
    }

    private static double? CalculateTotalAssetValues(IEnumerable<IndividualAssetsTotalModel> individualAssetsTotals)
    {
        double sum = individualAssetsTotals.Sum(x => x.Value);

        return sum is 0 ? null : sum;
    }
}
