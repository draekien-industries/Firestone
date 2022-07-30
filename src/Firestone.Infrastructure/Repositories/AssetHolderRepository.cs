namespace Firestone.Infrastructure.Repositories;

using Application.Common.Repositories;
using Application.FireProgressionTable.Contracts;
using Data;
using Domain.Data;

public class AssetHolderRepository : IAssetHolderRepository
{
    private readonly IFirestoneDbContext _context;

    public AssetHolderRepository(IFirestoneDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<AssetHolder> AddAsync(
        Guid tableId,
        NewAssetHolderDto assetHolderDetails,
        CancellationToken cancellationToken)
    {
        AssetHolder assetHolder = new(
            tableId,
            assetHolderDetails.Name,
            assetHolderDetails.MonthlyIncome,
            assetHolderDetails.PlannedMonthlyContribution);

        await _context.AssetHolders.AddAsync(assetHolder, cancellationToken);
        await _context.SaveChangeAsync(cancellationToken);

        return assetHolder;
    }
}
