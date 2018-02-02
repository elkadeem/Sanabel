using Sanabel.Security.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Sanabel.Security.Infra
{
    internal class UsersConfiguration : EntityTypeConfiguration<User>
    {
        public UsersConfiguration()
        {
            this.ToTable("Users").HasKey(c => c.Id);
            this.Property(c => c.UserName).IsRequired().HasMaxLength(50);
            this.Property(c => c.Email).IsRequired().HasMaxLength(50);
            this.Property(c => c.PasswordHash).HasMaxLength(200);
            this.Property(c => c.SecurityStamp).HasMaxLength(200);
            this.Property(c => c.PhoneNumber).HasMaxLength(20);
            this.Property(c => c.LockedOutDate).HasColumnType("datetime2");
            this.Property(c => c.FullName).HasMaxLength(100);
            

            this.HasMany(c => c.ExternalLogins);
            this.HasMany(c => c.Claims);
            this.HasMany(c => c.Roles)
                .WithMany()
                .Map(c =>
                {
                    c.MapLeftKey("UserId");
                    c.MapRightKey("RoleId");
                    c.ToTable("UserRoles");
                });
            
            this.HasIndex(c => c.UserName).HasName("IX_UserName").IsUnique();
            this.HasIndex(c => c.Email).HasName("IX_Email").IsUnique();
        }
    }
}
