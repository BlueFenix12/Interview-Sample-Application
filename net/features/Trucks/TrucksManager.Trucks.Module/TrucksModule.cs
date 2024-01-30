using Autofac;
using MediatR;
using TrucksManager.Trucks.CQRS.Queries.Ping;

namespace TrucksManager.Trucks.Module
{
    public class TrucksModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            ConfigureFluentValidations(builder);
            RegisterMediatrGenericHandlers(builder);
        }

        private static void RegisterMediatrGenericHandlers(ContainerBuilder builder)
        {
            // builder.RegisterGeneric(typeof(ValidationQuery<>))
            //     .As(typeof(IRequest<>))
            //     .InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(ValidationQueryRequestHandler<>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            // builder.RegisterAssemblyTypes(typeof(AssemblyMarker).Assembly)
            //     .Where(t => t.Name.EndsWith("QueryHandler", StringComparison.InvariantCultureIgnoreCase)
            //                 || t.Name.EndsWith("CommandHandler", StringComparison.InvariantCultureIgnoreCase))
            //     .AsImplementedInterfaces()
            //     .InstancePerLifetimeScope();
        }

        private static void ConfigureFluentValidations(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(AssemblyMarker).Assembly)
                .Where(t => t.Name.EndsWith("QueryValidator", StringComparison.InvariantCultureIgnoreCase)
                            || t.Name.EndsWith("CommandValidator", StringComparison.InvariantCultureIgnoreCase))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
