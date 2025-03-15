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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ;

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new DoctorConfiguration());
            modelBuilder.ApplyConfiguration(new AdminConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerServiceConfiguration());



        }
    }
}