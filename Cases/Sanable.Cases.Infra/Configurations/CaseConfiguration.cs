using Sanable.Cases.Domain.Model;
using System.Data.Entity.ModelConfiguration;

namespace Sanable.Cases.Infra
{
    public class CaseConfiguration : EntityTypeConfiguration<Case>
    {
        public CaseConfiguration()
        {
            this.ToTable("Cases").HasKey(c => c.Id);

            this.Property(c => c.CaseType).IsRequired();
            this.Property(c => c.Address).HasMaxLength(200);
            this.Property(c => c.CityId).IsRequired();
            this.Property(c => c.Description).HasMaxLength(1000);
            this.Property(c => c.Gender).IsRequired();
            this.Property(c => c.Name).IsRequired().HasMaxLength(100);
            this.Property(c => c.Phone).IsRequired().HasMaxLength(20);

            this.HasIndex(c => c.Name).HasName("IX_CaseName").IsUnique();
            this.HasIndex(c => c.Phone).HasName("IX_CasePhone").IsUnique();

            this.HasRequired(c => c.City).WithMany()
                .HasForeignKey(c => c.CityId).WillCascadeOnDelete(false);

            this.HasOptional(c => c.District).WithMany()
                .HasForeignKey(c => c.DistrictId).WillCascadeOnDelete(false);

            this.HasMany<CaseAction>(c => c.CaseActions)
                .WithRequired().HasForeignKey(c => c.CaseId)
                .WillCascadeOnDelete();
        }
    }
}
