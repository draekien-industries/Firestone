namespace Firestone.Application.Common.Repositories;

using Domain.Data;
using FireProgressionTableEntry.Commands;

public interface IFireProgressionTableEntryRepository
{
    Task<FireProgressionTableEntry> AddInitialInvestmentAsync(
        FireProgressionTable table,
        AddInitialInvestmentCommand command,
        CancellationToken cancellationToken);
}
