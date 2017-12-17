using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.DataAccessLayer
{
    public class SecurityContext : DbContext
    {
        public SecurityContext() : base("SecurityConnectionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SecurityContext
                , Security.DataAccessLayer.Migrations.Configuration>("SecurityConnectionString"));
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserLogin> UserLogins { get; set; }

        public DbSet<UserClaim> UserClaims { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany<Role>(c => c.Roles)
                .WithMany(c => c.Users)
                .Map(c =>
                {
                    c.MapLeftKey("UserId");
                    c.MapRightKey("RoleId");
                    c.ToTable("UserRoles", "Security");
                });

            modelBuilder.Entity<UserClaim>()
                .Property<int>(c => c.ClaimId)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

        }
    }


}
