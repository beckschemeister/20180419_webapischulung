namespace Mvc.Security.Roles.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Mvc.Security.Roles.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Mvc.Security.Roles.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Mvc.Security.Roles.Models.ApplicationDbContext";
        }

        protected override void Seed(Mvc.Security.Roles.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole { Name = "Admin" },
                new IdentityRole { Name = "Manager" });

            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            um.AddToRole("97ba7c3e-5e34-4c5e-9d9b-5e1ebe1fd97f", "Admin"); // test@test.com
            um.AddToRole("98825b90-05a4-4a3d-8de1-0dca48ff3f8b", "Manager"); // test2@test.com
        }
    }
}
