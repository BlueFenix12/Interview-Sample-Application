using TrucksManager.Trucks.Module.Configuration;

namespace TrucksApi.Configuration;

internal static class FluentValidationConfiguration
{
    public static void Configure(IServiceCollection services)
    {
        services.ConfigureValidatorsFromTruckModule();
    }
}