namespace Firestone.Application.LineItem.Contracts;

using Assets.Contracts;
using Domain.Models;
using Waystone.Common.Application.Contracts.Mappings;

public class LineItemDto : IMapFrom<LineItem>
{
    public Guid Id { get; set; }

    public Guid FireTableId { get; set; }

    public DateOnly Date { get; set; }

    public IEnumerable<AssetsDto> Assets { get; set; } = Array.Empty<AssetsDto>();

    public double TotalAssets => Assets.Sum(asset => asset.Amount);
}
