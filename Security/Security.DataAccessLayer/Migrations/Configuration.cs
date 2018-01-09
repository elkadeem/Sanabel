namespace Security.DataAccessLayer.Migrations
{
    using Security.Domain;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Security.DataAccessLayer.SecurityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Security.DataAccessLayer.SecurityContext context)
        {
            context.Roles.AddOrUpdate(c => c.RoleName
              , new Role[] {
                  new Role{ Id = Guid.NewGuid(), RoleName = "Administrator", RoleNameAr = "مدير النظام"},
                  new Role{ Id = Guid.NewGuid(), RoleName = "DataEntery", RoleNameAr = "مدخل بيانات"},
                  new Role{ Id = Guid.NewGuid(), RoleName = "Member", RoleNameAr = "عضو"},
                  new Role{ Id = Guid.NewGuid(), RoleName = "MainBoard", RoleNameAr = "مجلس الإدارة"},
                  new Role{ Id = Guid.NewGuid(), RoleName = "FinancialCommitte", RoleNameAr = "اللجنة المالية"},
                  new Role{ Id = Guid.NewGuid(), RoleName = "MedicalCommitte", RoleNameAr = "اللجنة الطبية"},
                  new Role{ Id = Guid.NewGuid(), RoleName = "LegalCommittee", RoleNameAr = "اللجنة القانونية"},                  
              });

            context.SaveChanges();
        }
    }
}
