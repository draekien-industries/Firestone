namespace Firestone.Infrastructure.Data;

using Domain.Models;
using Microsoft.EntityFrameworkCore;

public class FirestoneDbContext : DbContext
{
    public FirestoneDbContext(DbContextOptions<FirestoneDbContext> options) : base(options)
    { }

    public DbSet<FireTable> FireTables => Set<FireTable>();

    public DbSet<AssetHolder> AssetHolders => Set<AssetHolder>();

    public DbSet<Assets> Assets => Set<Assets>();

    public DbSet<LineItem> LineItems => Set<LineItem>();


    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FirestoneDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
