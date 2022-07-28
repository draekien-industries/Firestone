namespace Firestone.Domain.Models;

using Constants;
using Data;

public class NominalReturnRateModel : EntityDomainModel<NominalReturnRateConfiguration>
{
    public NominalReturnRateModel(Guid tableId, double yearlyReturnRate)
    {
        TableId = tableId;
        YearlyReturnRate = yearlyReturnRate;
    }

    public NominalReturnRateModel(NominalReturnRateConfiguration entity) : base(entity.Id)
    {
        TableId = entity.TableId;
        YearlyReturnRate = entity.YearlyReturnRate;
    }

    public Guid TableId { get; }

    public double YearlyReturnRate { get; private set; }

    public double MonthlyReturnRate => YearlyReturnRate / FirestoneValues.MonthsPerYear;

    public void UpdateYearlyRate(double newYearlyRate)
    {
        YearlyReturnRate = newYearlyRate;
    }

    /// <inheritdoc />
    protected override NominalReturnRateConfiguration CreateEntity()
    {
        NominalReturnRateConfiguration entity = new()
        {
            TableId = TableId,
            YearlyReturnRate = YearlyReturnRate,
        };

        return entity;
    }
}
