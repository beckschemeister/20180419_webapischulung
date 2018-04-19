using System.Web.Http;
using Api.AdventureWorks2012.Productmanagement.App_Start;
using AutoMapper;

namespace Api.AdventureWorks2012.Productmanagement
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
