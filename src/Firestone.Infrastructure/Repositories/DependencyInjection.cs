namespace Firestone.Infrastructure.Repositories;

using Application.AssetHolder.Services;
using Application.Assets.Services;
using Application.FireTable.Services;
using Application.LineItem.Services;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IFireTableRepository, FireTableRepository>();
        services.AddScoped<IAssetHolderRepository, AssetHolderRepository>();
        services.AddScoped<ILineItemRepository, LineItemRepository>();
        services.AddScoped<IAssetsRepository, AssetsRepository>();

        return services;
    }
}
