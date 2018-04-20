using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc.Security.Roles.App_Start
{
    public static class RoleGroups
    {
        public const string CanSeeThings = "Admin,Manager";

        public const string CanDoThings = "Admin";
    }
}