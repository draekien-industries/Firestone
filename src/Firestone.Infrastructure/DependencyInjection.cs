namespace Firestone.Infrastructure;

using Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddWaystoneInfrastructureBuilder()
                .AcceptDefaults();

        services.AddDataAccess(configuration);
        services.AddRepositories();

        return services;
    }
}
