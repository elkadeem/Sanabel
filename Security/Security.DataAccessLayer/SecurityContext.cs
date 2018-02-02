using Sanabel.Security.Domain;
using System.Data.Entity;

namespace Sanabel.Security.Infra
{
    public class SecurityContext : DbContext
    {
        public SecurityContext() : base("SecurityConnectionString")
        {
            Database.SetInitializer<SecurityContext>(null);
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<ExternalLogin> ExternalLogins { get; set; }

        public DbSet<Claim> Claims { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Security");
            modelBuilder.Configurations.Add(new UsersConfiguration());
            modelBuilder.Configurations.Add(new RolesConfiguration());
            modelBuilder.Configurations.Add(new ExternalLoginsConfiguration());
            modelBuilder.Configurations.Add(new ClaimsConfiguration());            
        }
    }
}
