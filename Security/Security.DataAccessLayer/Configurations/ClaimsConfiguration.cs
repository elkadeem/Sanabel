using Sanabel.Security.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sanabel.Security.Infra
{
    internal class ClaimsConfiguration : EntityTypeConfiguration<Claim>
    {
        public ClaimsConfiguration()
        {
            this.ToTable("UserClaims").HasKey(c => c.ClaimId)
                .Property(c => c.ClaimId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(c => c.ClaimType).IsRequired().HasMaxLength(200);
            this.Property(c => c.ClaimValue).HasMaxLength(100);
            
            this.HasIndex(c => new { c.UserId, c.ClaimType, c.ClaimValue}).HasName("IX_UserClaim").IsUnique();
        }
    }
}
