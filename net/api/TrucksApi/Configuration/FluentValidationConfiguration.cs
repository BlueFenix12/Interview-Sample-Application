using TrucksManager.Trucks.Module.Configuration;

namespace TrucksApi.Configuration;

public static class FluentValidationConfiguration
{
    public static void ConfigureFluentValidations(this IServiceCollection services)
    {
        services.ConfigureValidatorsFromTruckModule();
    }
}