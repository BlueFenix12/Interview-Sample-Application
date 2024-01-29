using Autofac.Extensions.DependencyInjection;
using Autofac;
using TrucksManager.Trucks.Module;

namespace TrucksApi.Configuration;

public static class AutofacConfiguration
{
    public static void ConfigureAutofacContainer(this ConfigureHostBuilder host)
    {
        host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

        host.ConfigureContainer<ContainerBuilder>(builder =>
        {
            builder.RegisterModule(new TrucksModule());
        });
    }
}
