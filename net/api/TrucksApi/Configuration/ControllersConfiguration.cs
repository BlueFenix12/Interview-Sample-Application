using TrucksApi.Configuration.ApiVersioning;
using TrucksManager.Trucks.Module.ApiConfiguration;

namespace TrucksApi.Configuration;

public static class ControllersConfiguration
{
    public static void ConfigureControllers(this IServiceCollection services)
    {
        services
            .AddControllers(options => options.UseRoutePrefix("api/v{version:apiVersion}"))
            .AddTrucksModuleParts();
    }
}
