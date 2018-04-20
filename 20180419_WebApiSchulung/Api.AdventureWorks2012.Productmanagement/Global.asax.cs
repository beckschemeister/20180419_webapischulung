using System.Web.Http;
using System.Web.Mvc;
using Api.AdventureWorks2012.Productmanagement.App_Start;
using Api.AdventureWorks2012.Productmanagement.Filters;
using AutoMapper;

namespace Api.AdventureWorks2012.Productmanagement
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Mapper.Initialize(c => c.AddProfile<MappingProfile>()); // AutoMapper initialisieren

            GlobalConfiguration.Configuration.Filters.Add(new CustomExceptionFilterAttribute()); // Global alle Exceptions durch diesen Filter schicken.

            GlobalConfiguration.Configure(WebApiConfig.Register);

            AreaRegistration.RegisterAllAreas(); // Helppages von M$ initialisieren.
        }
    }
}
