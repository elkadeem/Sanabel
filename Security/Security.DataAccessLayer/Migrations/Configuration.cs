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
                  new Role("Administrator", "���� ������"),
                  new Role("DataEntery", "���� ������"),
                  new Role("Member", "���"),
                  new Role("MainBoard", "���� �������"),
                  new Role("FinancialCommitte", "������ �������"),
                  new Role("MedicalCommitte", "������ ������"),
                  new Role("LegalCommittee", "������ ���������"),
              });

            context.SaveChanges();
        }
    }
}
