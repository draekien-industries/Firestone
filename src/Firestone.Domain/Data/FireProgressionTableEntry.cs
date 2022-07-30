namespace Firestone.Domain.Data;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Utils;

public class FireProgressionTableEntry : Entity
{
    private FireProgressionTableEntry()
    { }

    /// <summary>
    /// Creates an initial investment line item.
    /// </summary>
    /// <param name="table"></param>
    /// <param name="entryDateTime"></param>
    /// <param name="retirementTargetConfiguration"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public FireProgressionTableEntry(
        FireProgressionTable table,
        DateTime entryDateTime,
        RetirementTargetConfiguration retirementTargetConfiguration)
    {
        TableId = table.Id;
        DateTime = entryDateTime;
        RetirementTargetValue = retirementTargetConfiguration.TargetValue;
        CoastTargetValue = table.InitialCoastTarget
                        ?? throw new InvalidOperationException("Initial coast target was not available from the table");
    }

    /// <summary>
    /// Creates placeholder table entries ready to be populated with individual assets.
    /// </summary>
    /// <param name="table"></param>
    /// <param name="entryDateTime"></param>
    /// <param name="inflationRateConfiguration"></param>
    /// <param name="nominalReturnRateConfiguration"></param>
    /// <param name="previous"></param>
    public FireProgressionTableEntry(
        FireProgressionTable table,
        DateTime entryDateTime,
        InflationRateConfiguration inflationRateConfiguration,
        NominalReturnRateConfiguration nominalReturnRateConfiguration,
        FireProgressionTableEntry previous)
    {
        TableId = table.Id;
        DateTime = entryDateTime;

        RetirementTargetValue = MathUtils.IncreaseByPercentage(
            previous.RetirementTargetValue,
            inflationRateConfiguration.MonthlyRate);

        CoastTargetValue = MathUtils.IncreaseByPercentage(
            previous.CoastTargetValue,
            nominalReturnRateConfiguration.MonthlyReturnRate);

        IndividualAssetValues = new List<IndividualAssetsTotal>();
        ProjectedTotalAssetValues = new List<ProjectedAssetsTotal>();

        if (!previous.MinimumGrowthTargetValue.HasValue) return;

        MinimumGrowthTargetValue = MathUtils.IncreaseByPercentage(
                                       previous.MinimumGrowthTargetValue.Value,
                                       nominalReturnRateConfiguration.MonthlyReturnRate)
                                 + table.MinimumMonthlyContributionValue;
    }

    [ForeignKey(nameof(FireProgressionTable))]
    [Description("The table this entry belongs to")]
    public Guid TableId { get; set; }

    [ForeignKey(nameof(Previous))]
    [Description("The previous entry in the table")]
    public Guid? PreviousTableEntryId { get; set; }

    [Description("The month the table entry corresponds to.")]
    public DateTime DateTime { get; set; }

    [NotMapped]
    [Description("The month the table entry corresponds to.")]
    public DateOnly Date => DateOnly.FromDateTime(DateTime);

    [Description("The retirement target value at the date of this entry.")]
    public double RetirementTargetValue { get; set; }

    [Description("The coast target value at the date of this entry.")]
    public double CoastTargetValue { get; set; }

    [Description("The minimum growth rate of total asset values at the date of this entry.")]
    public double? MinimumGrowthTargetValue { get; set; }

    [NotMapped]
    [Description("The actual total asset values at the date of this entry.")]
    public double? TotalAssetValues => GetTotalAssetValues();

    [NotMapped]
    [Description("The change in total asset values (compared to the previous entry).")]
    public double? ChangeInTotalAssetValues => GetChangeInTotalAssetValues();

    public virtual FireProgressionTable FireProgressionTable { get; set; } = default!;

    public virtual FireProgressionTableEntry? Previous { get; set; }

    public virtual ICollection<IndividualAssetsTotal> IndividualAssetValues { get; set; } =
        new List<IndividualAssetsTotal>();

    public virtual ICollection<ProjectedAssetsTotal> ProjectedTotalAssetValues { get; set; } =
        new List<ProjectedAssetsTotal>();

    private double? GetTotalAssetValues()
    {
        return !IndividualAssetValues.Any() ? default(double?) : IndividualAssetValues.Sum(x => x.Value);
    }

    private double? GetChangeInTotalAssetValues()
    {
        if (Previous is null) return default;

        return ChangeInTotalAssetValues - Previous.ChangeInTotalAssetValues;
    }
}
