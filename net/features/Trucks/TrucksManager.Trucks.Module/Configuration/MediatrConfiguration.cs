using Microsoft.Extensions.DependencyInjection;

namespace TrucksManager.Trucks.Module.Configuration;

public static class MediatrConfiguration
{
    public static void ConfigureMediatrOfTrucksModule(this MediatRServiceConfiguration configuration)
    {
        configuration.RegisterServicesFromAssembly(typeof(AssemblyMarker).Assembly);
    }
}