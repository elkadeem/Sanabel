using CommonSettings.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSettings.DAL
{
    public class CountryConfiguration : EntityTypeConfiguration<Country>
    {
        public CountryConfiguration()
        {
            this.ToTable("Countries").HasKey(c => c.Id)
                .Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(c => c.Name).HasMaxLength(50).IsRequired();
            this.Property(c => c.NameEn).HasMaxLength(50).IsOptional();
            this.Property(c => c.Code).HasMaxLength(10).IsRequired();

            this.HasIndex(c => c.Name).HasName("IX_CountryName").IsUnique();
            this.HasIndex(c => c.Code).HasName("IX_CountryCode").IsUnique();
            this.HasMany(c => c.Regions);
        }

    }
}
