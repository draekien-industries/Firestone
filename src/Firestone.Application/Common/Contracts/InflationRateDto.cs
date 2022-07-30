namespace Firestone.Application.Common.Contracts;

using Domain.Data;
using Waystone.Common.Application.Contracts.Mappings;

public class InflationRateDto : IMapFrom<InflationRateConfiguration>
{
    public Guid Id { get; set; }

    public Guid TableId { get; set; }

    public double YearlyRate { get; set; }

    public double MonthlyRate { get; set; }
}
