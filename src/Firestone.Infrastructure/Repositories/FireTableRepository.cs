namespace Firestone.Infrastructure.Repositories;

using Application.FireTable.Services;
using Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Waystone.Common.Domain.Contracts.Exceptions;

public class FireTableRepository : IFireTableRepository
{
    private readonly FirestoneDbContext _context;

    public FireTableRepository(FirestoneDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public Task<int> CountAsync(CancellationToken cancellationToken)
    {
        return _context.FireTables.CountAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<FireTable> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        FireTable? result =
            await _context.FireTables
                          .Include(x => x.AssetHolders)
                          .Include(x => x.LineItems)
                          .ThenInclude(x => x.Assets)
                          .AsSplitQuery()
                          .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (result is null) throw new NotFoundException(typeof(FireTable), id.ToString());

        return result;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<FireTable>> ListAsync(
        int cursor,
        int limit,
        CancellationToken cancellationToken)
    {
        List<FireTable> results = await _context.FireTables
                                                .Include(table => table.AssetHolders)
                                                .OrderBy(table => table.Id)
                                                .Skip(cursor)
                                                .Take(limit)
                                                .ToListAsync(cancellationToken);

        return results;
    }

    /// <inheritdoc />
    public async Task<FireTable> AddAsync(
        string name,
        double yearlyInflationRate,
        double yearlyNominalReturnRate,
        double retirementTarget,
        int yearsToRetirement,
        CancellationToken cancellationToken)
    {
        FireTable table = FireTable.Initialise(
            name,
            yearlyInflationRate,
            yearlyNominalReturnRate,
            retirementTarget,
            yearsToRetirement);

        await _context.FireTables.AddAsync(table, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return table;
    }
}
