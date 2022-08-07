namespace Firestone.Infrastructure.Repositories;

using Application.Assets.Services;
using Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Waystone.Common.Domain.Contracts.Exceptions;

internal class AssetsRepository : IAssetsRepository
{
    private readonly FirestoneDbContext _context;

    public AssetsRepository(FirestoneDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<Assets> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        Assets? assets = await _context.Assets.FirstOrDefaultAsync(assets => assets.Id.Equals(id), cancellationToken);

        if (assets is null)
        {
            throw new NotFoundException(typeof(LineItem), id.ToString());
        }

        return assets;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Assets>> AddRangeAsync(
        Guid lineItemId,
        IReadOnlyDictionary<Guid, double> assets,
        CancellationToken cancellationToken)
    {
        List<Assets> assetsToAdd =
            assets.Select(asset => Assets.Initialise(lineItemId, asset.Key, asset.Value)).ToList();

        await _context.Assets.AddRangeAsync(assetsToAdd, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return assetsToAdd;
    }

    /// <inheritdoc />
    public async Task<Assets> UpdateAsync(Guid id, double amount, CancellationToken cancellationToken)
    {
        Assets assets = await GetAsync(id, cancellationToken);

        assets.Amount = amount;

        _context.Assets.Update(assets);
        await _context.SaveChangesAsync(cancellationToken);

        return assets;
    }
}
