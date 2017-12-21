using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.DataAccessLayer;

namespace Security.IntegrationTest
{
    public class SecurityDatabaseInitializar : DropCreateDatabaseAlways<Security.DataAccessLayer.SecurityContext>
    {
        public SecurityDatabaseInitializar()
        {

        }

        protected override void Seed(SecurityContext context)
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
