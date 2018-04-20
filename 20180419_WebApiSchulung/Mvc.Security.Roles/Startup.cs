using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mvc.Security.Roles.Startup))]
namespace Mvc.Security.Roles
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
