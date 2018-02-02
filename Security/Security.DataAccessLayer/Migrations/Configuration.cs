namespace Security.DataAccessLayer.Migrations
{
    using Sanabel.Security.Domain;
    using System;
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<Sanabel.Security.Infra.SecurityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Sanabel.Security.Infra.SecurityContext context)
        {
            context.Roles.AddOrUpdate(c => c.Name
              , new Role[] {
                  new Role{ Id = Guid.NewGuid(), Name = "Administrator", NameAr = "مدير النظام"},
                  new Role{ Id = Guid.NewGuid(), Name = "DataEntery", NameAr = "مدخل بيانات"},
                  new Role{ Id = Guid.NewGuid(), Name = "Member", NameAr = "عضو"},
                  new Role{ Id = Guid.NewGuid(), Name = "MainBoard", NameAr = "مجلس الإدارة"},
                  new Role{ Id = Guid.NewGuid(), Name = "FinancialCommitte", NameAr = "اللجنة المالية"},
                  new Role{ Id = Guid.NewGuid(), Name = "MedicalCommitte", NameAr = "اللجنة الطبية"},
                  new Role{ Id = Guid.NewGuid(), Name = "LegalCommittee", NameAr = "اللجنة القانونية"},                  
              });

            context.SaveChanges();
        }
    }
}
