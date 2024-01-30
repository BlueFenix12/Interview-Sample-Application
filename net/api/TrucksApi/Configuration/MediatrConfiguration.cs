using MediatR;
using TrucksManager.Common;
using TrucksManager.Common.CQRS;
using TrucksManager.Trucks.Module.Configuration;

namespace TrucksApi.Configuration;

public static class MediatrConfiguration
{
    public static void ConfigureMediatR(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        services.AddMediatR(configuration =>
        {
            configuration.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            configuration.ConfigureMediatrOfTrucksModule();
        });
    }
}