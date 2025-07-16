using DentalClinicManagement.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalClinicManagement.InfrastructureLayer.Configuration
{
    public class CustomerServiceConfiguration : IEntityTypeConfiguration<CustomerService>
    {
        public void Configure(EntityTypeBuilder<CustomerService> builder)
        {
            builder.ToTable("CustomerServices");

            builder.HasKey(cs => cs.Id);

            builder.Property(cs => cs.FullName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(d => d.WorkingTime)
                 .HasMaxLength(300);

            builder.Property(cs => cs.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(cs => cs.Password)
                   .IsRequired();

            builder.Property(cs => cs.PhoneNumber)
                   .HasMaxLength(20);

            // One-to-Many relationship: Many CustomerService users can belong to one Role
            builder.HasOne(cs => cs.Role)
                   .WithMany(r => r.CustomerServices)
                   .HasForeignKey(cs => cs.RoleId)
                   .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete
            builder.HasMany(cs => cs.Patients)
                .WithOne(p => p.CustomerService)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(cs => cs.Appointments)
       .WithOne(a => a.CustomerService)
       .HasForeignKey(a => a.CustomerServiceId)
       .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(cs => cs.Payments)
       .WithOne(pay => pay.CustomerService)
       .HasForeignKey(pay => pay.CustomerServiceId)
       .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
