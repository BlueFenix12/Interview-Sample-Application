using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TrucksApi.Configuration.Swagger;

public class SwaggerConfigureOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public SwaggerConfigureOptions(IApiVersionDescriptionProvider provider)
    {
        this._provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach(var description in this._provider.ApiVersionDescriptions)
        { 
            options.SwaggerDoc(description.GroupName, CreateOpenApiInfoFromDescription(description));
        }
    }

    private static OpenApiInfo CreateOpenApiInfoFromDescription(ApiVersionDescription description)
    {
        OpenApiInfo openApiInfo = new()
        {
            Title = "Trucks API",
            Version = description.ApiVersion.ToString(),
        };

        if (description.IsDeprecated)
        {
            openApiInfo.Description += "The API version has been deprecated";
        }

        return openApiInfo;
    }
}
