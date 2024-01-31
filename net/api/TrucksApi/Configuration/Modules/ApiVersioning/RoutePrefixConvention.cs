using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace TrucksApi.Configuration.ApiVersioning;

public class RoutePrefixConvention : IApplicationModelConvention
{
    private readonly AttributeRouteModel routePrefix;

    public RoutePrefixConvention(IRouteTemplateProvider routeTemplateProvider)
    {
        this.routePrefix = new AttributeRouteModel(routeTemplateProvider);
    }

    public void Apply(ApplicationModel application)
    {
        foreach(var selector in application.Controllers.SelectMany(c => c.Selectors))
        {
            selector.AttributeRouteModel = selector.AttributeRouteModel != null
                ? AttributeRouteModel.CombineAttributeRouteModel(this.routePrefix, selector.AttributeRouteModel)
                : this.routePrefix;
        }
    }
}
