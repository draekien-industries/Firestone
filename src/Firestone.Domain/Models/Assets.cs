namespace Firestone.Domain.Models;

public class Assets : DbEntity
{
    public Guid AssetHolderId { get; set; }

    public Guid LineItemId { get; set; }

    public double Amount { get; set; }

    public AssetHolder AssetHolder { get; set; } = default!;

    public LineItem LineItem { get; set; } = default!;

    public static Assets Initialise(Guid lineItemId, Guid assetHolderId, double amount)
    {
        return new Assets
        {
            AssetHolderId = assetHolderId,
            LineItemId = lineItemId,
            Amount = amount,
        };
    }
}
