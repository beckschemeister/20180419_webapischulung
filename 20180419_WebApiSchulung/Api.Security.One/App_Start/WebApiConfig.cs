using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Api.Security.One
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Das musste wieder auskommentiert werden, da wir in Startup.Owin.cs das CORS enabled haben. 
            // Und scheinbar gibt zweimal enabled = disabled?!?! 
            //var cors = new EnableCorsAttribute("*", "*", "*");
            //config.EnableCors(cors);

            // Web-API-Konfiguration und -Dienste

            // Web-API-Routen
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
