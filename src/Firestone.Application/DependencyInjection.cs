namespace Firestone.Application;

using FireProgressionTable.Repositories;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddWaystoneApplicationBuilder(typeof(DependencyInjection))
                .AcceptDefaults();

        services.AddScoped<IFireProgressionTableRepository, FireProgressionTableRepository>();

        return services;
    }
}
