namespace Firestone.Application.Common.Contracts;

using Domain.Data;
using Waystone.Common.Application.Contracts.Mappings;

public class NominalReturnRateDto : IMapFrom<NominalReturnRateConfiguration>
{
    public Guid Id { get; set; }

    public Guid TableId { get; set; }

    public double YearlyReturnRate { get; set; }

    public double MonthlyReturnRate { get; set; }
}
