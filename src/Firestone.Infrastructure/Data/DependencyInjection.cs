namespace Firestone.Infrastructure.Data;

using Application.Common.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Firestone");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Firestone connection string is not configured");
        }

        services.AddDbContext<IFirestoneDbContext, FirestoneDbContext>(
            options =>
            {
                void SetMigrationAssembly(SqlServerDbContextOptionsBuilder builder)
                {
                    builder.MigrationsAssembly(typeof(FirestoneDbContext).Assembly.FullName);
                }

                options.UseSqlServer(
                    connectionString,
                    SetMigrationAssembly);
            });

        return services;
    }
}
