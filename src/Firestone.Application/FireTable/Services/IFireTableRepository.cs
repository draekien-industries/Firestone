namespace Firestone.Application.FireTable.Services;

using Domain.Models;

public interface IFireTableRepository
{
    Task<FireTable> GetAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<FireTable>> ListAsync(
        int cursor,
        int limit,
        CancellationToken cancellationToken);

    Task<FireTable> AddAsync(
        double yearlyInflationRate,
        double yearlyNominalReturnRate,
        double retirementTarget,
        int yearsToRetirement,
        CancellationToken cancellationToken);

    Task<int> CountAsync(CancellationToken cancellationToken);
}
