using MediatR;
using MediatR.Pipeline;
using TrucksManager.Common;
using TrucksManager.Common.CQRS;
using TrucksManager.Common.CQRS.Handlers;
using TrucksManager.Trucks.Module.Configuration;

namespace TrucksApi.Configuration;

internal static class MediatrConfiguration
{
    public static void Configure(IServiceCollection services)
    {
        services.AddTransient(typeof(IRequestExceptionHandler<,,>), typeof(GlobalRequestExceptionHandler<,,>));
        
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggerPipelineBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        services.AddMediatR(configuration =>
        {
            configuration.AddOpenBehavior(typeof(LoggerPipelineBehavior<,>));
            configuration.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            
            configuration.ConfigureMediatrOfTrucksModule();
        });
    }
}