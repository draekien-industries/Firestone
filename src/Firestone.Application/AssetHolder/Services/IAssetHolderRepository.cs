namespace Firestone.Application.AssetHolder.Services;

using Domain.Models;

/// <summary>
/// Repository for accessing asset holders.
/// </summary>
public interface IAssetHolderRepository
{
    /// <summary>
    /// Get an asset holder by ID.
    /// </summary>
    /// <param name="id">The ID of the asset holder.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken" /></param>
    /// <returns>The <see cref="CancellationToken" /></returns>
    Task<AssetHolder> GetAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new asset holder associated to a <see cref="FireTable" />
    /// </summary>
    /// <param name="tableId">The ID of the <see cref="FireTable" /></param>
    /// <param name="name">The name of the asset holder.</param>
    /// <param name="expectedMonthlyIncome">Their expected monthly income.</param>
    /// <param name="plannedMonthlyContribution">Their planned monthly contribution towards assets.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken" /></param>
    /// <returns>The created <see cref="AssetHolder" /></returns>
    Task<AssetHolder> AddAsync(
        Guid tableId,
        string name,
        double expectedMonthlyIncome,
        double plannedMonthlyContribution,
        CancellationToken cancellationToken);
}
