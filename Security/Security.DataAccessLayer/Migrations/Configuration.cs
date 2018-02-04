namespace Sanabel.Security.Infra.Migrations
{
    using Sanabel.Security.Domain;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class SecurityContextConfiguration : DbMigrationsConfiguration<Sanabel.Security.Infra.SecurityContext>
    {
        public SecurityContextConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "SecurityContextConfiguration";
        }

        protected override void Seed(Sanabel.Security.Infra.SecurityContext context)
        {
            context.Roles.AddOrUpdate(c => c.Name
              , new Role[] {
                  new Role("Administrator", "مدير النظام"),
                  new Role("DataEntery", "مدخل بيانات"),
                  new Role("Member", "عضو"),
                  new Role("MainBoard", "مجلس الإدارة"),
                  new Role("FinancialCommitte", "اللجنة المالية"),
                  new Role("MedicalCommitte", "اللجنة الطبية"),
                  new Role("LegalCommittee", "اللجنة القانونية"),
              });

            context.SaveChanges();
        }
    }
}
