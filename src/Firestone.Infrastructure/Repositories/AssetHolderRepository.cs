namespace Firestone.Infrastructure.Repositories;

using Application.AssetHolder.Services;
using Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Waystone.Common.Domain.Contracts.Exceptions;

public class AssetHolderRepository : IAssetHolderRepository
{
    private readonly FirestoneDbContext _context;

    public AssetHolderRepository(FirestoneDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<AssetHolder> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        AssetHolder? assetHolder = await _context.AssetHolders
                                                 .Include(assetHolder => assetHolder.Assets)
                                                 .FirstOrDefaultAsync(
                                                      assetHolder => assetHolder.Id.Equals(id),
                                                      cancellationToken);

        if (assetHolder is null)
        {
            throw new NotFoundException(typeof(AssetHolder), id.ToString());
        }

        return assetHolder;
    }

    /// <inheritdoc />
    public async Task<AssetHolder> AddAsync(
        Guid tableId,
        string name,
        double expectedMonthlyIncome,
        double plannedMonthlyContribution,
        CancellationToken cancellationToken)
    {
        AssetHolder assetHolder = AssetHolder.Initialise(
            tableId,
            name,
            expectedMonthlyIncome,
            plannedMonthlyContribution);

        await _context.AssetHolders.AddAsync(assetHolder, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return assetHolder;
    }
}
