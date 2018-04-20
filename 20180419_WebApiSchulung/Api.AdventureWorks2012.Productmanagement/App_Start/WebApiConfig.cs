using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json.Serialization;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using Api.AdventureWorks2012.Productmanagement.Models;
using Api.AdventureWorks2012.Productmanagement.Controllers;

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
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // OData Routing
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Product>(nameof(ProductOdataController).Replace("Controller", ""));
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

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
