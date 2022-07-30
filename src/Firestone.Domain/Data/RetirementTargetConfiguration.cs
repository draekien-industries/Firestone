namespace Firestone.Domain.Data;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Constants;

public class RetirementTargetConfiguration : Entity
{
    private RetirementTargetConfiguration()
    { }

    public RetirementTargetConfiguration(
        Guid tableId,
        int yearsUntilRetirement,
        double targetValue)
    {
        TableId = tableId;
        YearsUntilRetirement = yearsUntilRetirement;
        TargetValue = targetValue;
    }

    [Description("The number of years (from the initial investment) that you wish to retire in.")]
    public int YearsUntilRetirement { get; set; }

    [NotMapped]
    [Description("The number of months (from the initial investment) that you wish to retire in.")]
    public int MonthsUntilRetirement => YearsUntilRetirement * FirestoneValues.MonthsPerYear;

    [Description("The amount of money you will need before retiring (at the point of initial investment).")]
    public double TargetValue { get; set; }

    [ForeignKey(nameof(FireProgressionTable))]
    public Guid TableId { get; set; }

    public virtual FireProgressionTable FireProgressionTable { get; set; } = default!;
}
