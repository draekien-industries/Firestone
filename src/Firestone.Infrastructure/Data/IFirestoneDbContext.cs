namespace Firestone.Infrastructure.Data;

using Domain.Data;
using Microsoft.EntityFrameworkCore;

public interface IFirestoneDbContext
{
    DbSet<AssetHolder> AssetHolders { get; }

    DbSet<IndividualAssetsTotal> IndividualAssetsTotals { get; }

    DbSet<InflationRateConfiguration> InflationRates { get; }

    DbSet<NominalReturnRateConfiguration> NominalReturnRates { get; }

    DbSet<PlannedIndividualContributionConfiguration> PlannedIndividualContributions { get; }

    DbSet<ProjectedAssetsTotal> ProjectedAssetsTotals { get; }

    DbSet<RetirementTargetConfiguration> RetirementTargets { get; }

    DbSet<FireProgressionTable> FireProgressionTables { get; }

    DbSet<FireProgressionTableEntry> FireProgressionTableEntries { get; }

    Task<int> SaveChangeAsync(CancellationToken cancellationToken = default);
}
