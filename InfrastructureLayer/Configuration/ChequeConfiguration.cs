using DentalClinicManagement.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DentalClinicManagement.InfrastructureLayer.Configuration
{
    public class ChequeConfiguration : IEntityTypeConfiguration<Cheque>
    {
        public void Configure(EntityTypeBuilder<Cheque> builder)
        {
            

            builder.HasKey(c => c.Id);

            builder.Property(c => c.BankName)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(c => c.IssueDate)
                   .IsRequired();

            builder.Property(c => c.DueDate)
                   .IsRequired();

            // Relationship: one payment → many cheques
            builder.HasOne(c => c.Payment)
                   .WithMany(p => p.Cheques)
                   .HasForeignKey(c => c.PaymentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
