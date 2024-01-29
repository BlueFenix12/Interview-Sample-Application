﻿using Microsoft.Extensions.DependencyInjection;

namespace TrucksManager.Trucks.Module;

public static class TrucksMvcBuilder
{
    public static IMvcBuilder AddTrucksModuleParts(this IMvcBuilder builder)
    {
        return builder.AddApplicationPart(typeof(TrucksManager.Trucks.Api.AssemblyMarker).Assembly);
    }
}
