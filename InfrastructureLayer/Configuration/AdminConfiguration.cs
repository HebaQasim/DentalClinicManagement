using DentalClinicManagement.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalClinicManagement.InfrastructureLayer.Configuration
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.ToTable("Admins");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.FirstName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(a => a.LastName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(a => a.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(a => a.Password)
                   .IsRequired();

            builder.Property(a => a.PhoneNumber)
                   .HasMaxLength(20);

            // One-to-One relationship between Admin and Role
            builder.HasOne(a => a.Role)
                   .WithOne(r => r.Admin)
                   .HasForeignKey<Admin>(a => a.RoleId)
                   .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete


            var adminRoleId = new Guid("de4f6736-fa9a-48b3-b788-fc5506bedf08");
            builder.HasData(new Admin
            {
                Id = new Guid("ce93f771-e6b7-46b8-af59-66251bc1998f"),
                FirstName = "Admin",
                LastName = "Admin",
                Email = "hebaalqerem2003@gmail.com",
                Password = "AQAAAAIAAYagAAAAEBUw0I4lTSGxDbeOl5dKGrDzf6rdHkGf4deUAiwvwOXIlSrQHyidITXkAH92YI2qIg==", // Hashed Password
                PhoneNumber = "1234567890",
                RoleId = adminRoleId
            });
        }
    }
}

