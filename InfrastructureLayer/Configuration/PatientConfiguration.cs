using DentalClinicManagement.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DentalClinicManagement.InfrastructureLayer.Configuration
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {

            builder.ToTable("Patient");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.DOB)
                   .IsRequired();

            builder.Property(p => p.Address)
                   .HasMaxLength(200);

            builder.Property(p => p.Phone)
                   .HasMaxLength(20);

            builder.Property(p => p.EmergencyContact)
                   .HasMaxLength(20);

            builder.Property(p => p.Gender)
                   .HasMaxLength(20);

            builder.Property(p => p.Job)
                   .HasMaxLength(100);

            // Relationship with CustomerService
            builder.HasOne(p => p.CustomerService)
                   .WithMany(cs => cs.Patients) // add ICollection<Patient> in CustomerService if you want navigation
                   .HasForeignKey(p => p.CustomerServiceId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Many-to-Many with Doctors
            builder.HasMany(p => p.Doctors)
                   .WithMany(d => d.Patients);

            builder.HasMany(p => p.Appointments)
       .WithOne(a => a.Patient)
       .HasForeignKey(a => a.PatientId)
       .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Payments)
       .WithOne(pay => pay.Patient)
       .HasForeignKey(pay => pay.PatientId)
       .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
