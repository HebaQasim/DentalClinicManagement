using DentalClinicManagement.DomainLayer.Entities;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.InfrastructureLayer.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DentalClinicManagement.InfrastructureLayer.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DentalClinicDbContext _context;
        private readonly IPasswordHasher<Admin> _adminPasswordHasher;
        private readonly IPasswordHasher<Doctor> _doctorPasswordHasher;
        private readonly IPasswordHasher<CustomerService> _csPasswordHasher;

        public AuthRepository(
        DentalClinicDbContext context,
        IPasswordHasher<Admin> adminPasswordHasher,
        IPasswordHasher<Doctor> doctorPasswordHasher,
        IPasswordHasher<CustomerService> csPasswordHasher)
        {
            _context = context;
            _adminPasswordHasher = adminPasswordHasher;
            _doctorPasswordHasher = doctorPasswordHasher;
            _csPasswordHasher = csPasswordHasher;
        }

        public async Task<(object? User, string Role)?> AuthenticateAsync(
            string email, string password, CancellationToken cancellationToken = default)
        {
            // Check the admin table 
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Email == email, cancellationToken);
            if (admin is not null && _adminPasswordHasher.VerifyHashedPassword(admin, admin.Password, password) == PasswordVerificationResult.Success)
            {
                return (admin, "Admin");
            }

            // Check the doctors table 
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Email == email, cancellationToken);
            if (doctor is not null && _doctorPasswordHasher.VerifyHashedPassword(doctor, doctor.Password, password) == PasswordVerificationResult.Success)
            {
                return (doctor, "Doctor");
            }

            // Check CustomerService table 
            var customerService = await _context.CustomerServices.FirstOrDefaultAsync(cs => cs.Email == email, cancellationToken);
            if (customerService is not null && _csPasswordHasher.VerifyHashedPassword(customerService, customerService.Password, password) == PasswordVerificationResult.Success)
            {
                return (customerService, "CustomerService");
            }

            return null;
        }
    }

}
