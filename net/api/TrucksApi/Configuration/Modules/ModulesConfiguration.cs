using TrucksManager.Trucks.Module;

namespace TrucksApi.Configuration;

internal static class ModulesConfiguration
{
    public static void Configure(IServiceCollection services)
    {
        services.ConfigureTrucksModule();
    }
}