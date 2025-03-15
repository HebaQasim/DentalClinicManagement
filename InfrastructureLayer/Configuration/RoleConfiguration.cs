using DentalClinicManagement.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalClinicManagement.InfrastructureLayer.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id); // Set the primary key

            builder.Property(r => r.Name)
                   .IsRequired()
                   .HasMaxLength(50); // Ensures the role name is required and has a max length

            // Relationships
            builder.HasMany(r => r.Doctors)  // One-to-many relationship with Doctors
                   .WithOne(d => d.Role)  // Each Doctor has one Role
                   .HasForeignKey(d => d.RoleId)  // Define the foreign key in the Doctor entity
                   .OnDelete(DeleteBehavior.Restrict);  // Prevent cascading delete

            builder.HasMany(r => r.CustomerServices)  // One-to-many relationship with CustomerServices
                   .WithOne(cs => cs.Role)  // Each CustomerService has one Role
                   .HasForeignKey(cs => cs.RoleId)  // Define the foreign key in the CustomerService entity
                   .OnDelete(DeleteBehavior.Restrict);  // Prevent cascading delete

            builder.HasOne(r => r.Admin)  // One-to-one relationship with Admin
                   .WithOne(a => a.Role)  // One Role is associated with one Admin
                   .HasForeignKey<Admin>(a => a.RoleId)  // Define the foreign key in the Admin entity
                   .OnDelete(DeleteBehavior.Restrict); // Cascade delete for Admin

            // Seed some data for Roles
            builder.HasData(
                new Role { Id = new Guid("de4f6736-fa9a-48b3-b788-fc5506bedf08"), Name = "Admin" },
                new Role { Id = new Guid("2b7e1f1e-8f4c-4f3a-b7d7-3f2e8f5a9e4c"), Name = "Doctor" },
                new Role { Id = new Guid("3c2e4d3b-9f5a-4a1d-bb5e-8a2e1f7c9d3f"), Name = "CustomerService" }
            );
        }
    }
}
