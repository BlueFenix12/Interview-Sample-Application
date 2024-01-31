namespace TrucksApi.Configuration;

public static class ApiConfiguration
{
    public static void ConfigureApi(this IServiceCollection services)
    {
        ControllersConfiguration.Configure(services);
        ApiVersioningConfiguration.Configure(services);
        SwaggerConfiguration.Configure(services);
        MediatrConfiguration.Configure(services);
        FluentValidationConfiguration.Configure(services);
        DatabaseConfiguration.Configure(services);
        ModulesConfiguration.Configure(services);
    }
}