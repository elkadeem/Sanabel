using Sanable.Cases.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanable.Cases.Infra
{
    public class CaseResearchDataContext : DbContext
    {
        public CaseResearchDataContext() : base("CasesConnectionString")
        {
        }

        public DbSet<Case> Cases { get; set; }
        
        public DbSet<CaseResearch> CaseResearches { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Cases");
            modelBuilder.Configurations.Add(new CaseConfiguration());
            modelBuilder.Configurations.Add(new CaseReserachConfiguration());
        }
    }
}
