namespace Firestone.Application.Assets.Services;

using Domain.Models;

/// <summary>
/// Repository for accessing <see cref="Assets" />
/// </summary>
public interface IAssetsRepository
{
    /// <summary>
    /// Gets an assets record by it's ID.
    /// </summary>
    /// <param name="id">The ID fo the assets entry.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken" /></param>
    /// <returns>The <see cref="Assets" /></returns>
    Task<Assets> GetAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Adds a collection of assets from multiple asset holders to a FIRE table line item.
    /// </summary>
    /// <param name="lineItemId">The ID of the line item.</param>
    /// <param name="assets">The collection of asset values by asset holder id.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken" /></param>
    /// <returns>The collection of created <see cref="Assets" /></returns>
    Task<IEnumerable<Assets>> AddRangeAsync(
        Guid lineItemId,
        IReadOnlyDictionary<Guid, double> assets,
        CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing assets record
    /// </summary>
    /// <param name="id">The ID of the assets record</param>
    /// <param name="amount">The new assets amount</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken" /></param>
    /// <returns>The updated <see cref="Assets" /></returns>
    Task<Assets> UpdateAsync(Guid id, double amount, CancellationToken cancellationToken);
}
