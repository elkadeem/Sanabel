using Security.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.DataAccessLayer
{
    internal class RolesConfiguration : EntityTypeConfiguration<Role>
    {
        public RolesConfiguration()
        {
            this.ToTable("Roles").HasKey(c => c.Id);
            this.Property(c => c.Name).IsRequired().HasMaxLength(50);
            this.Property(c => c.NameAr).IsRequired().HasMaxLength(50);
            
            this.HasIndex(c => c.Name).HasName("IX_RoleName").IsUnique();
        }
    }
}
