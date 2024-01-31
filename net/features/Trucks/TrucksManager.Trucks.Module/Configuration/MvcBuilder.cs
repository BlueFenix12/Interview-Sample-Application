using Microsoft.Extensions.DependencyInjection;

namespace TrucksManager.Trucks.Module.Configuration;

public static class MvcBuilder
{
    public static IMvcBuilder AddTrucksModuleParts(this IMvcBuilder builder)
    {
        return builder.AddApplicationPart(typeof(TrucksManager.Trucks.Api.AssemblyMarker).Assembly);
    }
}
