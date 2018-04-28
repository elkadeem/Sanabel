using CommonSettings.Domain.Entities;
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
            Database.SetInitializer<CaseResearchDataContext>(null);
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Case> Cases { get; set; }
        
        public DbSet<CaseResearch> CaseResearches { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Cases");
            modelBuilder.Configurations.Add(new CaseConfiguration());
            modelBuilder.Configurations.Add(new CaseReserachConfiguration());
            modelBuilder.Entity<Country>().ToTable("Countries", "Common");
            modelBuilder.Entity<Region>().ToTable("Regions", "Common");
            modelBuilder.Entity<City>().ToTable("Cities", "Common");
            modelBuilder.Entity<District>().ToTable("Districts", "Common");
        }
    }
}
