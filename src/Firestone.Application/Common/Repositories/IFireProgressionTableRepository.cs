namespace Firestone.Application.Common.Repositories;

using Domain.Data;
using FireProgressionTable.Commands;

public interface IFireProgressionTableRepository
{
    Task<FireProgressionTable> GetAsync(Guid id, CancellationToken cancellationToken);

    Task<FireProgressionTable> AddAsync(InitializeTableCommand command, CancellationToken cancellationToken);
}
