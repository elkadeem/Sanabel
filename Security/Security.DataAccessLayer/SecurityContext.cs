using Security.DataAccessLayer.Migrations;
using Security.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.DataAccessLayer
{
    public class SecurityContext : DbContext
    {
        public SecurityContext() : base("SecurityConnectionString")
        {
            Database.SetInitializer<SecurityContext>(null);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<ExternalLogin> ExternalLogins { get; set; }

        public DbSet<Claim> Claims { get; set; }

        public DbQuery<City> Cities => Set<City>().AsNoTracking();

        public DbQuery<District> Districts => Set<District>().AsNoTracking();

        public DbQuery<Country> Countries => Set<Country>().AsNoTracking();

        public DbQuery<Region> Regions => Set<Region>().AsNoTracking();

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Security");
            modelBuilder.Configurations.Add(new UsersConfiguration());
            modelBuilder.Configurations.Add(new RolesConfiguration());
            modelBuilder.Configurations.Add(new ExternalLoginsConfiguration());
            modelBuilder.Configurations.Add(new ClaimsConfiguration());

            modelBuilder.Entity<Country>().ToTable("Countries", "Common");
            modelBuilder.Entity<Region>().ToTable("Regions", "Common");
            modelBuilder.Entity<City>().ToTable("Cities", "Common");
            modelBuilder.Entity<District>().ToTable("Districts", "Common");
        }
    }
}
