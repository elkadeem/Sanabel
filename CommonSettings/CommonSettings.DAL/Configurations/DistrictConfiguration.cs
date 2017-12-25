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
    public class CityConfiguration : EntityTypeConfiguration<City>
    {
        public CityConfiguration()
        {
            this.ToTable("Cities").HasKey(c => c.Id)                
                .HasRequired(c => c.Region);
            this.HasMany(c => c.Districts);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Code).HasMaxLength(10);
            this.Property(c => c.Name).IsRequired().HasMaxLength(50);
            this.Property(c => c.NameEn).HasMaxLength(50);
            this.Property(c => c.RegionId).IsRequired();
        }
    }
}
