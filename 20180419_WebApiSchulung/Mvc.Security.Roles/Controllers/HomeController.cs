using Mvc.Security.Roles.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Security.Roles.Controllers
{
    [Authorize(Roles = RoleGroups.CanSeeThings)]
    public class HomeController : Controller
    {
        [AllowAnonymous] // jeder darf hier drauf
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize(Roles = RoleGroups.CanDoThings)] // Nur Admins
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}