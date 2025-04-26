using DentalClinicManagement.DomainLayer.Entities;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.DomainLayer.Interfaces.IServices;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DentalClinicManagement.ApplicationLayer.Common.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand>
    {
        private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;
        private readonly IEmailService _emailService;
        private readonly IAdminRepository _adminRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly ICustomerServiceRepository _customerServiceRepository;

        public ForgotPasswordCommandHandler(
            IPasswordResetTokenRepository passwordResetTokenRepository,
            IEmailService emailService,
            IAdminRepository adminRepository,
            IDoctorRepository doctorRepository,
            ICustomerServiceRepository customerServiceRepository, IPasswordHasher<Admin> adminPasswordHasher,
            IPasswordHasher<Doctor> doctorPasswordHasher,
            IPasswordHasher<CustomerService> customerServicePasswordHasher)
        {
            _passwordResetTokenRepository = passwordResetTokenRepository;
            _emailService = emailService;
            _adminRepository = adminRepository;
            _doctorRepository = doctorRepository;
            _customerServiceRepository = customerServiceRepository;

        }

        public async Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var email = request.Email.Trim().ToLower();

            var adminExists = await _adminRepository.ExistsByEmailAsync(email);
            var doctorExists = await _doctorRepository.ExistsByEmailAsync(email);
            var customerServiceExists = await _customerServiceRepository.ExistsByEmailAsync(email);

            if (!adminExists && !doctorExists && !customerServiceExists)
            {
                throw new KeyNotFoundException("No user found with this email.");
            }
            // إنشاء توكن عشوائي
            var token = Guid.NewGuid().ToString();

            var resetToken = new PasswordResetToken
            {
                Email = email,
                Token = token,
                ExpirationTime = DateTime.UtcNow.AddHours(1),
                IsUsed = false
            };

            await _passwordResetTokenRepository.AddAsync(resetToken);

            // بناء رابط إعادة التعيين
            var resetLink = $"https://yourdomain.com/reset-password?token={token}";

            // إرسال الإيميل
            await _emailService.SendEmailAsync(email, "Reset your password",
                $"Click the link below to reset your password:\n{resetLink}");
        }
    }
}
