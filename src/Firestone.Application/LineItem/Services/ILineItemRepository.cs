namespace Firestone.Application.LineItem.Services;

using Domain.Models;

public interface ILineItemRepository
{
    Task<LineItem> GetAsync(Guid id, CancellationToken cancellationToken);

    Task<LineItem> AddAsync(
        Guid fireTableId,
        DateTime date,
        CancellationToken cancellationToken);
}
