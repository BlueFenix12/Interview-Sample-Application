﻿using System.Text.Json.Serialization;
using TrucksApi.Configuration.ApiVersioning;
using TrucksManager.Trucks.Module.Configuration;

namespace TrucksApi.Configuration;

internal static class ControllersConfiguration
{
    public static void Configure(IServiceCollection services)
    {
        services
            .AddControllers(options => options.UseRoutePrefix("api/v{version:apiVersion}"))
            .AddJsonOptions(x => x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
            .AddTrucksModuleParts();
    }
}
