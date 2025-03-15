using DentalClinicManagement.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalClinicManagement.InfrastructureLayer.Configuration
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            // Define the table name
            builder.ToTable("Doctors");

            // Primary Key
            builder.HasKey(d => d.Id);

            // Properties
            builder.Property(d => d.FirstName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(d => d.LastName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(d => d.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(d => d.Password)
                   .IsRequired();

            builder.Property(d => d.PhoneNumber)
                   .HasMaxLength(15);

            builder.Property(d => d.Specialization)
                   .IsRequired()
                   .HasMaxLength(100);

            // Relationship: Each Doctor has one Role (Many-to-One)
            builder.HasOne(d => d.Role)
                   .WithMany(r => r.Doctors)
                   .HasForeignKey(d => d.RoleId)
                   .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes
        }
    }
}
