using Sanable.Cases.Domain.Model;
using System.Data.Entity.ModelConfiguration;

namespace Sanable.Cases.Infra
{
    public class CaseFollowerConfiguration : EntityTypeConfiguration<CaseFollower>
    {
        public CaseFollowerConfiguration()
        {
            this.ToTable("CaseFollowers").HasKey(c => c.Id)
                .HasRequired(c => c.CaseResearch)
                .WithMany(c => c.CaseFollowers)
                .HasForeignKey(c => c.CaseResearchId)
                .WillCascadeOnDelete();

            this.Property(c => c.CaseResearchId).IsRequired();
            this.Property(c => c.Name).IsRequired()
                .HasMaxLength(100);
            this.Property(c => c.BirthDate).HasColumnName("datetime2");
            this.Property(c => c.Education).HasMaxLength(100);
            this.Property(c => c.JobName).HasMaxLength(100);
            this.Property(c => c.Notes).HasMaxLength(500);
            this.Property(c => c.HealthStatusDescription).HasMaxLength(500);
            this.Property(c => c.HealthNotes).HasMaxLength(500);
        }
    }
}
