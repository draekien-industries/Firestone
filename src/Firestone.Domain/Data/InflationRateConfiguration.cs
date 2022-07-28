namespace Firestone.Domain.Data;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Constants;

public class InflationRateConfiguration : EntityBase
{
    [Description("The yearly inflation rate in decimal format.")]
    public double YearlyRate { get; set; }

    [NotMapped]
    [Description("The calculated monthrly inflation rate in decimal format.")]
    public double MonthlyRate => YearlyRate / FirestoneValues.MonthsPerYear;

    [ForeignKey(nameof(FireProgressionTable))]
    public Guid TableId { get; set; }

    public virtual FireProgressionTable FireProgressionTable { get; set; } = default!;
}
