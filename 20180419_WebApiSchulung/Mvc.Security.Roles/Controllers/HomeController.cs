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

            // User können auch direkt abgefragt werden 
            if (this.User.Identity.Name.Equals("chuck.norris") || !this.User.IsInRole("Admin"))
            {
                // ein ganz spezielles Recht
                ViewBag.Message += " hier noch dazu!";
            }
            
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