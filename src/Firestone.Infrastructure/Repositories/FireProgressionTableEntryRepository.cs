namespace Firestone.Infrastructure.Repositories;

using Application.Common.Repositories;
using Application.FireProgressionTableEntry.Commands;
using Data;
using Domain.Data;

public class FireProgressionTableEntryRepository : IFireProgressionTableEntryRepository
{
    private readonly IFirestoneDbContext _context;

    public FireProgressionTableEntryRepository(IFirestoneDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<FireProgressionTableEntry> AddInitialInvestmentAsync(
        FireProgressionTable table,
        AddInitialInvestmentCommand command,
        CancellationToken cancellationToken)
    {
        FireProgressionTableEntry entry = new(table, command.DateTime);

        await _context.FireProgressionTableEntries.AddAsync(entry, cancellationToken);
        await _context.SaveChangeAsync(cancellationToken);

        return entry;
    }
}
