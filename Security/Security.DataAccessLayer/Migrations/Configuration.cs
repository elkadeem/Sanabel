namespace Security.DataAccessLayer.Migrations
{
    using Security.Domain;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class SecurityMigrationsConfiguration : DbMigrationsConfiguration<Security.DataAccessLayer.SecurityContext>
    {
        public SecurityMigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Security.DataAccessLayer.SecurityContext context)
        {
            var roles = new List<Role>
            {
                new Role{ Id = Guid.NewGuid(), RoleName = "Administrator", RoleNameAr = "مدير النظام"},
                new Role{ Id = Guid.NewGuid(), RoleName = "User", RoleNameAr = "مستخدم"}
            };

            context.Roles.AddOrUpdate(roles.ToArray());
        }
    }
}
