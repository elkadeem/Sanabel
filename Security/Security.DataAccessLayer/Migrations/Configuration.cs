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
                  new Role("Administrator", "„œÌ— «·‰Ÿ«„"),
                  new Role("DataEntery", "„œŒ· »Ì«‰« "),
                  new Role("Member", "⁄÷Ê"),
                  new Role("MainBoard", "„Ã·” «·≈œ«—…"),
                  new Role("FinancialCommitte", "«··Ã‰… «·„«·Ì…"),
                  new Role("MedicalCommitte", "«··Ã‰… «·ÿ»Ì…"),
                  new Role("LegalCommittee", "«··Ã‰… «·ﬁ«‰Ê‰Ì…"),
              });

            context.SaveChanges();
        }
    }
}
