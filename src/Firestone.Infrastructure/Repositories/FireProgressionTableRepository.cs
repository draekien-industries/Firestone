namespace Firestone.Infrastructure.Repositories;

using Application.Common.Repositories;
using Application.FireProgressionTable.Commands;
using Application.FireProgressionTable.Queries;
using Data;
using Domain.Data;
using Microsoft.EntityFrameworkCore;
using Waystone.Common.Domain.Contracts.Exceptions;

public class FireProgressionTableRepository : IFireProgressionTableRepository
{
    private readonly IFirestoneDbContext _context;

    public FireProgressionTableRepository(IFirestoneDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<FireProgressionTable> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        FireProgressionTable? result =
            await _context.FireProgressionTables
                          .Include(x => x.InflationRateConfiguration)
                          .Include(x => x.NominalReturnRateConfiguration)
                          .Include(x => x.RetirementTargetConfiguration)
                          .Include(x => x.AssetHolders)
                          .ThenInclude(x => x.PlannedIndividualContributionConfiguration)
                          .Include(x => x.Entries)
                          .ThenInclude(x => x.IndividualAssetValues)
                          .Include(x => x.Entries)
                          .ThenInclude(x => x.ProjectedTotalAssetValues)
                          .AsSplitQuery()
                          .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (result is null) throw new NotFoundException(typeof(FireProgressionTable), id.ToString());

        return result;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<FireProgressionTable>> ListAsync(
        GetTablesQuery query,
        CancellationToken cancellationToken)
    {
        List<FireProgressionTable> results = await _context.FireProgressionTables
                                                           .Include(table => table.AssetHolders)
                                                           .Skip(query.Cursor)
                                                           .Take(query.Limit)
                                                           .ToListAsync(cancellationToken);

        return results;
    }

    /// <inheritdoc />
    public async Task<FireProgressionTable> AddAsync(
        InitializeTableCommand command,
        CancellationToken cancellationToken)
    {
        FireProgressionTable table = new(
            command.YearlyInflationRate,
            command.YearlyNominalReturnRate,
            command.YearsUntilRetirement,
            command.RetirementTarget);

        await _context.FireProgressionTables.AddAsync(table, cancellationToken);
        await _context.SaveChangeAsync(cancellationToken);

        return table;
    }

    /// <inheritdoc />
    public async Task UpdateProjectionsAsync(
        IEnumerable<FireProgressionTableEntry> projections,
        CancellationToken cancellationToken)
    {
        await _context.FireProgressionTableEntries.AddRangeAsync(projections, cancellationToken);
        await _context.SaveChangeAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task<int> CountAsync(CancellationToken cancellationToken)
    {
        return _context.FireProgressionTables.CountAsync(cancellationToken);
    }
}
