namespace Firestone.Domain.Models;

using Data;

public class IndividualAssetsTotalModel : EntityDomainModel<IndividualAssetsTotal>
{
    public IndividualAssetsTotalModel(Guid assetHolderId, double value)
    {
        AssetHolderId = assetHolderId;
        Value = value;
    }

    public IndividualAssetsTotalModel(Guid assetHolderId, Guid tableEntryId, double value)
    {
        AssetHolderId = assetHolderId;
        TableEntryId = tableEntryId;
        Value = value;
    }

    public IndividualAssetsTotalModel(IndividualAssetsTotal entity) : base(entity.Id)
    {
        AssetHolderId = entity.AssetHolderId;
        TableEntryId = entity.TableEntryId;
        Value = entity.Value;
    }

    public Guid AssetHolderId { get; }

    public Guid TableEntryId { get; }

    public double Value { get; }

    /// <inheritdoc />
    protected override IndividualAssetsTotal CreateEntity()
    {
        return new IndividualAssetsTotal
        {
            AssetHolderId = AssetHolderId,
            TableEntryId = TableEntryId,
            Value = Value,
        };
    }
}
