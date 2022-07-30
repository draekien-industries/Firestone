namespace Firestone.Domain.Data;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Constants;

public class NominalReturnRateConfiguration : Entity
{
    private NominalReturnRateConfiguration()
    { }

    public NominalReturnRateConfiguration(Guid tableId, double yearlyReturnRate)
    {
        TableId = tableId;
        YearlyReturnRate = yearlyReturnRate;
    }

    [Description("The yearly return on investment rate (in decimals) used to calculate projected asset values.")]
    public double YearlyReturnRate { get; set; }

    [NotMapped]
    [Description("The monthly return on investment rate (in decimals).")]
    public double MonthlyReturnRate => YearlyReturnRate / FirestoneValues.MonthsPerYear;

    [ForeignKey(nameof(FireProgressionTable))]
    public Guid TableId { get; set; }

    public virtual FireProgressionTable FireProgressionTable { get; set; } = default!;
}
