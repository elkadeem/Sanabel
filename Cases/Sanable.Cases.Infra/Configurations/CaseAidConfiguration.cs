using Sanable.Cases.Domain.Model;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sanable.Cases.Infra
{
    public class CaseAidConfiguration : EntityTypeConfiguration<Aid>
    {
        public CaseAidConfiguration()
        {
            this.ToTable("CaseAids");
            this.Property(c => c.AidDescription).HasMaxLength(1000)
                .IsRequired();
            this.Property(c => c.Notes).HasMaxLength(1000);
            this.Property(c => c.CreatedBy).HasMaxLength(50)
                .IsRequired();            
            this.Property(c => c.AidDate).HasColumnType("datetime2");
            this.Property(c => c.CreatedDate).HasColumnType("datetime2");
            this.Property(c => c.UpdatedDate).HasColumnType("datetime2");
            this.HasRequired(c => c.Case).WithMany(c => c.CaseAids);
        }
    }
}
