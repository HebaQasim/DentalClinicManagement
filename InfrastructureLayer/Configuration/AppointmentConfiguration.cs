using DentalClinicManagement.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DentalClinicManagement.InfrastructureLayer.Configuration
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointments");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Date)
                   .IsRequired();

            builder.Property(a => a.Time)
                   .IsRequired();

            builder.Property(a => a.Notes)
                   .HasMaxLength(500);

            // العلاقة مع Patient
            builder.HasOne(a => a.Patient)
                   .WithMany(p => p.Appointments)
                   .HasForeignKey(a => a.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);

            // العلاقة مع Doctor
            builder.HasOne(a => a.Doctor)
                   .WithMany(d => d.Appointments)
                   .HasForeignKey(a => a.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict);

            // العلاقة مع CustomerService
            builder.HasOne(a => a.CustomerService)
                   .WithMany(cs => cs.Appointments)
                   .HasForeignKey(a => a.CustomerServiceId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
