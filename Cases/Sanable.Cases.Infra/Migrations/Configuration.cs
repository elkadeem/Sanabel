namespace Sanable.Cases.Infra.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class CasesConfiguration : DbMigrationsConfiguration<Sanable.Cases.Infra.CaseResearchDataContext>
    {
        public CasesConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            this.ContextKey = "Cases";
        }

        protected override void Seed(Sanable.Cases.Infra.CaseResearchDataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
