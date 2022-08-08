namespace Firestone.Application;

using FireGraph.Services;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddWaystoneApplicationBuilder(typeof(DependencyInjection))
                .AcceptDefaults();

        services.AddScoped<IAssetsProjectionService, AssetsProjectionService>();
        services.AddScoped<ITargetAdjustmentService, TargetAdjustmentService>();

        return services;
    }
}
