﻿using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using TrucksApi.Configuration.Swagger;

namespace TrucksApi.Configuration;

internal static class SwaggerConfiguration
{
    public static void Configure(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigureOptions>();
        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type =>
            {
                var fullSchemaName = type.FullName.Replace("+", "_");
                return fullSchemaName;
            });
            options.OperationFilter<SwaggerOperationFilter>();
        });
    }

    public static void ConfigureSwaggerApplication(this WebApplication application)
    {
        application.UseSwagger();
        application.UseSwaggerUI(options =>
        {
            options.DocumentTitle = "Trucks API";
            
            
            IApiVersionDescriptionProvider apiProvider = application.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            foreach(var description in apiProvider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            }
        });
    }
}
