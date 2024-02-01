using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace TrucksManager.Trucks.Module.Configuration;

public static class ValidatorsConfiguration
{
    public static void ConfigureValidatorsFromTruckModule(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(AssemblyMarker).Assembly);
    }
}