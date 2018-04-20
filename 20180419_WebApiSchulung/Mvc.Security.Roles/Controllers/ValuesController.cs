using Mvc.Security.Roles.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mvc.Security.Roles.Controllers
{
    public class ValuesController : ApiController
    {
        [System.Web.Http.AuthorizeAttribute(Roles = RoleGroups.CanSeeThings)]
        public IHttpActionResult Get()
        {
            return Ok("Come in! :-)");
        }
    }
}
