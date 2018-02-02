using Sanabel.Security.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Sanabel.Security.Infra
{
    internal class ExternalLoginsConfiguration : EntityTypeConfiguration<ExternalLogin>
    {
        public ExternalLoginsConfiguration()
        {
            this.ToTable("UserLogins").HasKey(c => new { c.UserId, c.LoginProvider });
            this.Property(c => c.UserId).IsRequired();
            this.Property(c => c.LoginProvider).IsRequired().HasMaxLength(100);
            this.Property(c => c.ProviderKey).IsRequired().HasMaxLength(100);
            this.HasRequired(c => c.User);
        }
    }
}
