using DentalClinicManagement.DomainLayer.Entities;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;

namespace DentalClinicManagement.ApplicationLayer.Common.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
    {
        private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly ICustomerServiceRepository _customerServiceRepository;
        private readonly IPasswordHasher<Admin> _adminPasswordHasher;
        private readonly IPasswordHasher<Doctor> _doctorPasswordHasher;
        private readonly IPasswordHasher<CustomerService> _customerServicePasswordHasher;

        public ResetPasswordCommandHandler(
            IPasswordResetTokenRepository passwordResetTokenRepository,
            IAdminRepository adminRepository,
            IDoctorRepository doctorRepository,
            ICustomerServiceRepository customerServiceRepository, IPasswordHasher<Admin> adminPasswordHasher,
            IPasswordHasher<Doctor> doctorPasswordHasher,
            IPasswordHasher<CustomerService> customerServicePasswordHasher
           )
        {
            _passwordResetTokenRepository = passwordResetTokenRepository;
            _adminRepository = adminRepository;
            _doctorRepository = doctorRepository;
            _customerServiceRepository = customerServiceRepository;
           
            _adminPasswordHasher = adminPasswordHasher;
            _doctorPasswordHasher = doctorPasswordHasher;
            _customerServicePasswordHasher = customerServicePasswordHasher;
        }

        public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            // تحقق من صحة الإدخالات
            if (request.NewPassword != request.ConfirmPassword)
            {
                throw new ArgumentException("Passwords do not match.");
            }

            var resetToken = await _passwordResetTokenRepository.GetByTokenAsync(request.Token);
            if (resetToken == null || resetToken.ExpirationTime < DateTime.UtcNow || resetToken.IsUsed)
            {
                throw new UnauthorizedAccessException("Invalid or expired token.");
            }

            var email = resetToken.Email;

            var admin = await _adminRepository.GetByEmailAsync(email);
            if (admin != null)
            {
                admin.Password = _adminPasswordHasher.HashPassword(admin, request.NewPassword);
                await _adminRepository.UpdateAdminAsync(admin);
            }
            else
            {
                var doctor = await _doctorRepository.GetByEmailAsync(email);
                if (doctor != null)
                {
                    doctor.Password = _doctorPasswordHasher.HashPassword(doctor, request.NewPassword);
                    await _doctorRepository.UpdateDoctorAsync(doctor);
                }
                else
                {
                    var customerService = await _customerServiceRepository.GetByEmailAsync(email);
                    if (customerService != null)
                    {
                        customerService.Password = _customerServicePasswordHasher.HashPassword(customerService, request.NewPassword);
                        await _customerServiceRepository.UpdateCustomerServiceAsync(customerService);
                    }
                    else
                    {
                        throw new KeyNotFoundException("User not found.");
                    }
                }
            }

            // علمنا أن هذا التوكن تم استخدامه
            resetToken.IsUsed = true;
            await _passwordResetTokenRepository.UpdateAsync(resetToken);
        }
    }
}
