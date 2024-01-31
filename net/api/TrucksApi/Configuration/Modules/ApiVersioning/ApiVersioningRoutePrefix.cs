using Microsoft.AspNetCore.Mvc;

namespace TrucksApi.Configuration.ApiVersioning;

public static class ApiVersioningRoutePrefix
{
    public static void UseRoutePrefix(this MvcOptions options, string prefix)
    {
        var routeAttribute = new RouteAttribute(prefix);
        options.Conventions.Add(new RoutePrefixConvention(routeAttribute));
    }
}
