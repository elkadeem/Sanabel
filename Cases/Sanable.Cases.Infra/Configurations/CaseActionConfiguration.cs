using Sanable.Cases.Domain.Model;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sanable.Cases.Infra
{
    public class CaseActionConfiguration : EntityTypeConfiguration<CaseAction>
    {
        public CaseActionConfiguration()
        {
            this.ToTable("CaseActions");
            this.Property(c => c.Comment).HasMaxLength(1000);
            this.Property(c => c.CreatedBy).HasMaxLength(50);
            this.Property(c => c.CaseActionDate).HasColumnType("datetime2");
            this.Property(c => c.StartApplyDate).HasColumnType("datetime2");
        }
    }
}
