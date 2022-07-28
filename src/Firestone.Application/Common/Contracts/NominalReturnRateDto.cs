namespace Firestone.Application.Common.Contracts;

using Domain.Models;
using Waystone.Common.Application.Contracts.Mappings;

public class NominalReturnRateDto : IMapFrom<NominalReturnRateModel>
{
    public Guid Id { get; set; }

    public Guid TableId { get; set; }

    public double YearlyReturnRate { get; set; }

    public double MonthlyReturnRate { get; set; }
}
