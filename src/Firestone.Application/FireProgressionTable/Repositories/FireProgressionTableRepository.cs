namespace Firestone.Application.FireProgressionTable.Repositories;

using Common.Data;
using Domain.Data;
using Microsoft.EntityFrameworkCore;
using Waystone.Common.Domain.Contracts.Exceptions;

public interface IFireProgressionTableRepository
{
    Task<FireProgressionTable> GetAsync(Guid id, CancellationToken cancellationToken);
}

public class FireProgressionTableRepository
    : IFireProgressionTableRepository
{
    private readonly IFirestoneDbContext _context;

    public FireProgressionTableRepository(IFirestoneDbContext context)
    {
        _context = context;
    }

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
                          .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (result is null) throw new NotFoundException(typeof(FireProgressionTable), id.ToString());

        return result;
    }
}
