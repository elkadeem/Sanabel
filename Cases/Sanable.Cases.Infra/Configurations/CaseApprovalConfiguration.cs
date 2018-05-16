using Sanable.Cases.Domain.Model;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sanable.Cases.Infra
{
    public class CaseApprovalConfiguration : EntityTypeConfiguration<CaseApproval>
    {
        public CaseApprovalConfiguration()
        {
            this.ToTable("CaseApproval").HasKey(c => c.Id);

            this.Property(c => c.CaseId).IsRequired();
            this.Property(c => c.nApprovedBy).IsOptional();
            this.Property(c => c.dtApprovalDate).IsOptional();
            this.Property(c => c.nRejectedBy).IsOptional();
            this.Property(c => c.dtRejectionDate).IsOptional();
            this.Property(c => c.Comments).IsOptional();
            this.Property(c => c.nSuspendedBy).IsOptional();
            this.Property(c => c.dtSuspensionDate).IsOptional();
            this.HasRequired(c => c.CaseTable).WithMany()
                .HasForeignKey(c => c.CaseId).WillCascadeOnDelete(false);

    }
    }
}
