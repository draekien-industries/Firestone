namespace Firestone.Application.Common.Contracts;

using Domain.Models;
using Waystone.Common.Application.Contracts.Mappings;

public class InflationRateDto : IMapFrom<InflationRateModel>
{
    public Guid Id { get; set; }

    public Guid TableId { get; set; }

    public double YearlyRate { get; set; }

    public double MonthlyRate { get; set; }
}
