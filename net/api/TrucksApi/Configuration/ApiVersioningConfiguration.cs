using Asp.Versioning;
using Asp.Versioning.Conventions;

namespace TrucksApi.Configuration;

public static class ApiVersioningConfiguration
{
    public static void ConfigureApiVersioning(this IServiceCollection services)
    {
        services
            .AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
                options.ApiVersionParameterSource = new UrlSegmentApiVersionReader();
            })
            .AddMvc(options => 
            {
                options.Conventions.Add(new VersionByNamespaceConvention());
            });
    }
}
