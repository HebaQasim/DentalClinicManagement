using DentalClinicManagement.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DentalClinicManagement.InfrastructureLayer.Configuration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.PaymentDate)
                   .IsRequired();

            builder.Property(p => p.PaymentMethod)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(p => p.Notes)
                   .HasMaxLength(300);

            // Relationship with Patient
            builder.HasOne(p => p.Patient)
                   .WithMany(pa => pa.Payments)
                   .HasForeignKey(p => p.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relationship with CustomerService
            builder.HasOne(p => p.CustomerService)
                   .WithMany(cs => cs.Payments)
                   .HasForeignKey(p => p.CustomerServiceId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(p => p.Cheques)
                 .WithOne(c => c.Payment)
                 .HasForeignKey(c => c.PaymentId)
                 .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(p => p.Insurances)
                 .WithOne(i => i.Payment)
                 .HasForeignKey(c => c.PaymentId)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
