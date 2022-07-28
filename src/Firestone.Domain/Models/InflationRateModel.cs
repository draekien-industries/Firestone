namespace Firestone.Domain.Models;

using Constants;
using Data;

public class InflationRateModel : EntityDomainModel<InflationRateConfiguration>
{
    public InflationRateModel(Guid tableId, double yearlyRate)
    {
        TableId = tableId;
        YearlyRate = yearlyRate;
    }

    public InflationRateModel(InflationRateConfiguration entity) : base(entity.Id)
    {
        TableId = entity.TableId;
        YearlyRate = entity.YearlyRate;
    }

    public Guid TableId { get; }

    public double YearlyRate { get; private set; }

    public double MonthlyRate => YearlyRate / FirestoneValues.MonthsPerYear;

    public void UpdateYearlyRate(double newYearlyRate)
    {
        YearlyRate = newYearlyRate;
    }

    protected override InflationRateConfiguration CreateEntity()
    {
        InflationRateConfiguration entity = new()
        {
            TableId = TableId,
            YearlyRate = YearlyRate,
        };

        return entity;
    }
}
