using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.DataAccessLayer;
using Security.Domain;

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
                new Role{ Id = Guid.NewGuid(), Name = "Administrator", NameAr = "مدير النظام"},
                new Role{ Id = Guid.NewGuid(), Name = "User", NameAr = "مستخدم"}
            };

            context.Roles.AddOrUpdate(roles.ToArray());
        }
    }
}
