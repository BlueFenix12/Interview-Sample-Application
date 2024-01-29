using Serilog;

namespace TrucksApi.Configuration;

public static class SerilogConfiguration
{
    public static void ConfigureSerilog(this WebApplicationBuilder builder)
    {
        var serilogConfiguration = builder.Configuration
            .AddJsonFile("serilogSettings.json")
            .Build();

        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration.ReadFrom.Configuration(serilogConfiguration);
        });

    }
}
