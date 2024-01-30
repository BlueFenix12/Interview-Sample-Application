using TrucksManager.Trucks.Module;

namespace TrucksApi.Configuration;

public static class ModulesConfiguration
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.ConfigureTrucksModule();
    }
}