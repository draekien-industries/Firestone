namespace Firestone.Infrastructure.Repositories;

using Application.Common.Repositories;
using Application.FireProgressionTableEntry.Contracts;
using Data;
using Domain.Data;

public class IndividualAssetTotalsRepository : IIndividualAssetTotalsRepository
{
    private readonly IFirestoneDbContext _context;

    public IndividualAssetTotalsRepository(IFirestoneDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<IndividualAssetsTotal>> AddAsync(
        Guid tableEntryId,
        IEnumerable<IndividualAssetsSnapshotDto> assets,
        CancellationToken cancellationToken)
    {
        List<IndividualAssetsTotal> entities = assets.Select(
                                                          asset => new IndividualAssetsTotal(
                                                              asset.AssetHolderId,
                                                              tableEntryId,
                                                              asset.Value))
                                                     .ToList();

        await _context.IndividualAssetsTotals.AddRangeAsync(entities, cancellationToken);
        await _context.SaveChangeAsync(cancellationToken);

        return entities;
    }
}
