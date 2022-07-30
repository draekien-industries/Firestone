namespace Firestone.Infrastructure.Repositories;

using Application.Common.Repositories;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IFireProgressionTableRepository, FireProgressionTableRepository>();
        services.AddScoped<IFireProgressionTableEntryRepository, FireProgressionTableEntryRepository>();
        services.AddScoped<IAssetHolderRepository, AssetHolderRepository>();
        services.AddScoped<IIndividualAssetTotalsRepository, IndividualAssetTotalsRepository>();

        return services;
    }
}
