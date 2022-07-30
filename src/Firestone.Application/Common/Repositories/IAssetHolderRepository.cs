namespace Firestone.Application.Common.Repositories;

using Domain.Data;
using FireProgressionTable.Contracts;

public interface IAssetHolderRepository
{
    Task<AssetHolder> AddAsync(Guid tableId, NewAssetHolderDto assetHolderDetails, CancellationToken cancellationToken);
}
