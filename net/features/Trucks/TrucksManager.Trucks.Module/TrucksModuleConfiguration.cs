using Microsoft.Extensions.DependencyInjection;
using TrucksManager.Trucks.DataAccess;
using TrucksManager.Trucks.Infrastructure;

namespace TrucksManager.Trucks.Module;

public static class TrucksModuleConfiguration
{
    public static void ConfigureTrucksModule(this IServiceCollection services)
    {
        RegisterModuleServices(services);
    }

    private static void RegisterModuleServices(IServiceCollection services)
    {
        services.AddTransient<ITrucksRepository, TrucksRepository>();
    }
}