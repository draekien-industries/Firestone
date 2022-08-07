namespace Firestone.Infrastructure.Repositories;

using Application.LineItem.Services;
using Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Waystone.Common.Domain.Contracts.Exceptions;

internal class LineItemRepository : ILineItemRepository
{
    private readonly FirestoneDbContext _context;

    public LineItemRepository(FirestoneDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<LineItem> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        LineItem? lineItem = await _context.LineItems.Include(lineItem => lineItem.Assets)
                                           .AsSplitQuery()
                                           .FirstOrDefaultAsync(lineItem => lineItem.Id.Equals(id), cancellationToken);

        if (lineItem is null)
        {
            throw new NotFoundException(typeof(LineItem), id.ToString());
        }

        return lineItem;
    }

    /// <inheritdoc />
    public async Task<LineItem> AddAsync(
        Guid fireTableId,
        DateTime date,
        CancellationToken cancellationToken)
    {
        LineItem lineItem = LineItem.Initialise(fireTableId, date);

        await _context.LineItems.AddAsync(lineItem, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return lineItem;
    }
}
