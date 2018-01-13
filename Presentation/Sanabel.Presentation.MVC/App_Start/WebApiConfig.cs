using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Sanabel.Presentation.MVC
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "PlacesApi",
                routeTemplate: "Places/api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }
}
