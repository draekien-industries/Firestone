namespace Firestone.Application.FireProgressionTable.Repositories;

using Common.Data;
using Domain.Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Waystone.Common.Domain.Contracts.Exceptions;

public interface IFireProgressionTableRepository
{
    Task<FireProgressionTable> GetAsync(Guid id, CancellationToken cancellationToken);

    Task<FireProgressionTable> AddAsync(FireProgressionTableModel model, CancellationToken cancellationToken);
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
                          .Include(x => x.FireProgressionTableEntries)
                          .ThenInclude(x => x.IndividualAssetValues)
                          .Include(x => x.FireProgressionTableEntries)
                          .ThenInclude(x => x.ProjectedTotalAssetValues)
                          .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (result is null) throw new NotFoundException(typeof(FireProgressionTable), id.ToString());

        return result;
    }

    /// <inheritdoc />
    public async Task<FireProgressionTable> AddAsync(
        FireProgressionTableModel model,
        CancellationToken cancellationToken)
    {
        FireProgressionTable entity = model.ToEntity();

        await _context.FireProgressionTables.AddAsync(entity, cancellationToken);
        await _context.SaveChangeAsync(cancellationToken);

        return entity;
    }
}
