namespace Firestone.Application.FireGraph.Contracts;

using Domain.Models;
using Waystone.Common.Application.Contracts.Mappings;

public class DataPointDto : IMapFrom<DataPoint>
{
    public DateTime Date { get; set; }

    public double Amount { get; set; }
}
