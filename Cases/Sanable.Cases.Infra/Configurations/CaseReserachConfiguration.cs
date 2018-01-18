using Sanable.Cases.Domain.Model;
using System.Data.Entity.ModelConfiguration;

namespace Sanable.Cases.Infra
{
    public class CaseReserachConfiguration : EntityTypeConfiguration<CaseResearch>
    {
        public CaseReserachConfiguration()
        {
            this.ToTable("CaseReseraches").HasKey(c => c.Id)
                .HasRequired(c => c.Case)
                .WithMany(c => c.CaseResearchs)
                .HasForeignKey(c => c.CaseId)
                .WillCascadeOnDelete(false);

            this.HasRequired(c => c.Case).WithMany(c => c.CaseResearchs).HasForeignKey(c => c.CaseId);
            this.Property(c => c.CaseId).IsRequired();
            this.Property(c => c.ResearchDate).HasColumnType("datetime2");
            this.Property(c => c.MembershipDate).HasColumnType("datetime2");

            this.Property(c => c.OtherResearchType).HasMaxLength(50);
            this.Property(c => c.JobName).IsRequired().HasMaxLength(100);
            this.Property(c => c.QualificationName).IsRequired().HasMaxLength(50);
            this.Property(c => c.Address).IsRequired().HasMaxLength(200);
            this.Property(c => c.MembershipNo).HasMaxLength(20);
            this.Property(c => c.OtherAssociationName).HasMaxLength(50);
            this.Property(c => c.LeaveWorkReason).HasMaxLength(100);
            this.Property(c => c.NoIncomeReason).HasMaxLength(200);
            this.Property(c => c.ClosedRelativeName).HasMaxLength(100);
            this.Property(c => c.ClosedRelativePhone).HasMaxLength(20);
            this.Property(c => c.Description).HasMaxLength(500);
            this.Property(c => c.Skils).HasMaxLength(500);
            this.Property(c => c.NeededHelp).HasMaxLength(500);
            this.Property(c => c.HealthNotes).HasMaxLength(500);

        }
    }
}
