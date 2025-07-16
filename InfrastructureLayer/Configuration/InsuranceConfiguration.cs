using DentalClinicManagement.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DentalClinicManagement.InfrastructureLayer.Configuration
{
    public class InsuranceConfiguration : IEntityTypeConfiguration<Insurance>
    {
        public void Configure(EntityTypeBuilder<Insurance> builder)
        {
            builder.ToTable("Insurances");

            builder.HasKey(i => i.InsuranceId);

            builder.Property(i => i.Provider)
                   .IsRequired()
                   .HasMaxLength(150);

            // 🔗 Relationship: One Payment -> Many Insurances
            builder.HasOne(i => i.Payment)
                   .WithMany(p => p.Insurances)
                   .HasForeignKey(i => i.PaymentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
