using TrucksManager.Trucks.Module.ApiConfiguration;

namespace TrucksApi.Configuration;

public static class MediatrConfiguration
{
    public static void ConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(configurtion =>
        {
            configurtion.ConfigureMediatrOfTrucksModule();
        });
    }
}