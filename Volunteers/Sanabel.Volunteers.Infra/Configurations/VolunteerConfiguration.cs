using Sanabel.Volunteers.Domain.Model;
using System.Data.Entity.ModelConfiguration;

namespace Sanabel.Volunteers.Infra
{
    public class VolunteerConfiguration : EntityTypeConfiguration<Volunteer>
    {
        public VolunteerConfiguration()
        {
            ToTable("Volunteers", "Volunteers")
                .HasKey(c => c.Id);

            Property(c => c.Name).IsRequired().HasMaxLength(100);
            Property(c => c.Email).IsRequired().HasMaxLength(100);
            Property(c => c.Phone).IsRequired().HasMaxLength(20);
            Property(c => c.Notes).HasMaxLength(500);

            HasRequired(c => c.City)
                .WithMany()
                .WillCascadeOnDelete(false);

            HasOptional(c => c.District)
                .WithMany()
                .WillCascadeOnDelete(false);

            HasIndex(c => c.Name).HasName("IX_VolunteerName").IsUnique();
            HasIndex(c => c.Email).HasName("IX_VolunteerEmail").IsUnique();
            HasIndex(c => c.Phone).HasName("IX_VolunteerPhone").IsUnique();
        }
    }
}
