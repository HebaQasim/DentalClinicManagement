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

            builder.Property(cs => cs.FirstName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(cs => cs.LastName)
                   .IsRequired()
                   .HasMaxLength(50);

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



        }
    }
}
