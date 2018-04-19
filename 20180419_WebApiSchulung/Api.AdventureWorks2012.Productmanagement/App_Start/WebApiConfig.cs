using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json.Serialization;

namespace Api.AdventureWorks2012.Productmanagement
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Javascript Notation für JSON Response => Das heißt das dann u.a. die Properties des Models klein geschrieben werden.
            var settings = config.Formatters.JsonFormatter.SerializerSettings;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.Formatting = Newtonsoft.Json.Formatting.Indented;

            // CORS 
            var cors = new EnableCorsAttribute("*","*","*");
            config.EnableCors(cors);

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
