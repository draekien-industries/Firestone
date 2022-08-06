namespace Firestone.Application.Common.Repositories;

using Domain.Data;
using FireProgressionTable.Commands;
using FireProgressionTable.Queries;

public interface IFireProgressionTableRepository
{
    Task<FireProgressionTable> GetAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<FireProgressionTable>> ListAsync(
        GetTablesQuery query,
        CancellationToken cancellationToken);

    Task<FireProgressionTable> AddAsync(InitializeTableCommand command, CancellationToken cancellationToken);

    Task UpdateProjectionsAsync(
        IEnumerable<FireProgressionTableEntry> projections,
        CancellationToken cancellationToken);

    Task<int> CountAsync(CancellationToken cancellationToken);
}
