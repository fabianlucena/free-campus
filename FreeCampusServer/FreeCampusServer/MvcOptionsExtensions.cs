using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace FreeCampusServer
{
    public static class MvcOptionsExtensions
    {
        public static void UseGeneralRoutePrefix(this MvcOptions options, string prefix)
        {
            options.Conventions.Insert(0, new RoutePrefixConvention(new RouteAttribute(prefix)));
        }

        private class RoutePrefixConvention : IApplicationModelConvention
        {
            private readonly AttributeRouteModel _routePrefix;

            public RoutePrefixConvention(IRouteTemplateProvider route)
            {
                _routePrefix = new AttributeRouteModel(route);
            }

            public void Apply(ApplicationModel application)
            {
                foreach (var controller in application.Controllers)
                {
                    foreach (var selector in controller.Selectors)
                    {
                        if (selector.AttributeRouteModel != null)
                        {
                            selector.AttributeRouteModel =
                                AttributeRouteModel.CombineAttributeRouteModel(_routePrefix, selector.AttributeRouteModel);
                        }
                        else
                        {
                            selector.AttributeRouteModel = _routePrefix;
                        }
                    }
                }
            }
        }
    }
}