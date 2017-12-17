namespace Security.DataAccessLayer.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Security.DataAccessLayer.SecurityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Security.DataAccessLayer.SecurityContext context)
        {
            var roles = new List<Role>
            {
                new Role{ RoleId = Guid.NewGuid(), RoleName = "Administrator", RoleNameAr = "مدير النظام"},
                new Role{ RoleId = Guid.NewGuid(), RoleName = "User", RoleNameAr = "مستخدم"}
            };

            context.Roles.AddOrUpdate(roles.ToArray());
        }
    }
}
