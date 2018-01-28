using Security.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.DataAccessLayer
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
            this.Property(c => c.Address).HasMaxLength(200);

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

            this.HasRequired(c => c.City).WithMany(c => c.Users)
            .WillCascadeOnDelete(false);

            this.HasOptional(c => c.District).WithMany(c => c.Users)
                .WillCascadeOnDelete(false);

            this.HasIndex(c => c.UserName).HasName("IX_UserName").IsUnique();
            this.HasIndex(c => c.Email).HasName("IX_Email").IsUnique();
        }
    }
}
