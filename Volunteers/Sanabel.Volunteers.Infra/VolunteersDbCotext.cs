using CommonSettings.Domain.Entities;
using Sanabel.Volunteers.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanabel.Volunteers.Infra
{
    public class VolunteersDbCotext : DbContext
    {
        public VolunteersDbCotext() : base("name=VolunteersDbConnectionString")
        {
        }

        public DbSet<Volunteer> Volunteers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {            
            modelBuilder.Configurations.Add(new VolunteerConfiguration());
            modelBuilder.Entity<Country>().ToTable("Countries", "Common");
            modelBuilder.Entity<Region>().ToTable("Regions", "Common");
            modelBuilder.Entity<City>().ToTable("Cities", "Common");
            modelBuilder.Entity<District>().ToTable("Districts", "Common");
        }
    }
}
