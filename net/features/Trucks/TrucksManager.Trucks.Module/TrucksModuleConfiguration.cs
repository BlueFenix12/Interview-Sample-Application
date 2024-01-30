using Microsoft.Extensions.DependencyInjection;
using TrucksManager.Trucks.DataAccess;
using TrucksManager.Trucks.Infrastructure;

namespace TrucksManager.Trucks.Module;

public static class TrucksModuleConfiguration
{
    public static void ConfigureTrucksModule(this IServiceCollection services)
    {
        services.AddTransient<ITrucksRepository, TrucksRepository>();
    }
}