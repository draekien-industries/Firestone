namespace Firestone.Application.Common.Repositories;

using Domain.Data;
using FireProgressionTableEntry.Contracts;

public interface IIndividualAssetTotalsRepository
{
    Task<IEnumerable<IndividualAssetsTotal>> AddAsync(
        Guid tableEntryId,
        IEnumerable<IndividualAssetsSnapshotDto> assets,
        CancellationToken cancellationToken);
}
