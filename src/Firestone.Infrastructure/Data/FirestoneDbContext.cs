namespace Firestone.Infrastructure.Data;

using Application.Common.Data;
using Domain.Data;
using Microsoft.EntityFrameworkCore;

public class FirestoneDbContext : DbContext, IFirestoneDbContext
{
    public FirestoneDbContext(DbContextOptions<FirestoneDbContext> options) : base(options)
    { }

    public DbSet<AssetHolder> AssetHolders => Set<AssetHolder>();

    public DbSet<IndividualAssetsTotal> IndividualAssetsTotals => Set<IndividualAssetsTotal>();

    public DbSet<InflationRateConfiguration> InflationRates => Set<InflationRateConfiguration>();

    public DbSet<NominalReturnRateConfiguration> NominalReturnRates =>
        Set<NominalReturnRateConfiguration>();

    public DbSet<PlannedIndividualContributionConfiguration> PlannedIndividualContributions =>
        Set<PlannedIndividualContributionConfiguration>();

    public DbSet<ProjectedAssetsTotal> ProjectedAssetsTotals => Set<ProjectedAssetsTotal>();

    public DbSet<RetirementTargetConfiguration> RetirementTargets => Set<RetirementTargetConfiguration>();

    public DbSet<FireProgressionTable> FireProgressionTables => Set<FireProgressionTable>();

    public DbSet<FireProgressionTableEntry> FireProgressionTableEntries => Set<FireProgressionTableEntry>();

    /// <inheritdoc />
    public Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FireProgressionTable>()
                    .HasMany(table => table.FireProgressionTableEntries)
                    .WithOne(entry => entry.FireProgressionTable)
                    .OnDelete(DeleteBehavior.NoAction);

        base.OnModelCreating(modelBuilder);
    }
}
