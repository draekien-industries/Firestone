namespace Firestone.Domain.Models;

public class LineItem : DbEntity
{
    public Guid FireTableId { get; set; }

    public DateTime Date { get; set; }

    public FireTable FireTable { get; set; } = default!;

    public ICollection<Assets> Assets { get; set; } = new List<Assets>();

    public static LineItem Initialise(
        Guid fireTableId,
        DateTime date)
    {
        return new LineItem
        {
            FireTableId = fireTableId,
            Date = date,
        };
    }
}
