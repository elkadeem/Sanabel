using Security.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.DataAccessLayer
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
