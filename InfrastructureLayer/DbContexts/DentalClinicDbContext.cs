using Microsoft.EntityFrameworkCore;
using DentalClinicManagement.DomainLayer.Entities;
using DentalClinicManagement.InfrastructureLayer.Configuration;

namespace DentalClinicManagement.InfrastructureLayer.DbContexts
{
    public class DentalClinicDbContext : DbContext
    {
        public DentalClinicDbContext(DbContextOptions<DentalClinicDbContext> options)
        : base(options) { }


        public DbSet<Role> Roles { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<CustomerService> CustomerServices { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Cheque> Cheques { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new DoctorConfiguration());
            modelBuilder.ApplyConfiguration(new AdminConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerServiceConfiguration());
            modelBuilder.ApplyConfiguration(new TreatmentConfiguration());
            modelBuilder.ApplyConfiguration(new PatientConfiguration());
            modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            modelBuilder.ApplyConfiguration(new ChequeConfiguration());
            modelBuilder.ApplyConfiguration(new InsuranceConfiguration());


        }
    }
}