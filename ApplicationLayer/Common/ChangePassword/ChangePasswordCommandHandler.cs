using DentalClinicManagement.ApiLayer.Services;
using DentalClinicManagement.DomainLayer.Entities;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.DomainLayer.Interfaces.IServices;
using MediatR;

using Microsoft.AspNetCore.Identity;

namespace DentalClinicManagement.ApplicationLayer.Common.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordResponse>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly ICustomerServiceRepository _customerServiceRepository;
        private readonly IPasswordHasher<Admin> _adminPasswordHasher;
        private readonly IPasswordHasher<Doctor> _doctorPasswordHasher;
        private readonly IPasswordHasher<CustomerService> _customerServicePasswordHasher;
        private readonly IUserContext _userContext;
        private readonly IEmailService _emailService;

        public ChangePasswordCommandHandler(
            IAdminRepository adminRepository,
            IDoctorRepository doctorRepository,
            ICustomerServiceRepository customerServiceRepository,
            IPasswordHasher<Admin> adminPasswordHasher,
            IPasswordHasher<Doctor> doctorPasswordHasher,
            IPasswordHasher<CustomerService> customerServicePasswordHasher, IUserContext userContext,
             IEmailService emailService)

        {
            _adminRepository = adminRepository;
            _doctorRepository = doctorRepository;
            _customerServiceRepository = customerServiceRepository;
            _adminPasswordHasher = adminPasswordHasher;
            _doctorPasswordHasher = doctorPasswordHasher;
            _customerServicePasswordHasher = customerServicePasswordHasher;
            _userContext = userContext;
            _emailService = emailService;
        }

        public async Task<ChangePasswordResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.Id;
            var role = _userContext.Role;
            string email = string.Empty;
            string fullName = string.Empty;

            switch (role)
            {
                case "Admin":
                    var admin = await _adminRepository.GetAdminByIdAsync(userId, cancellationToken);
                    if (admin == null) throw new KeyNotFoundException("Admin not found.");

                    if (_adminPasswordHasher.VerifyHashedPassword(admin, admin.Password, request.CurrentPassword) == PasswordVerificationResult.Failed)
                        throw new UnauthorizedAccessException("Current password is incorrect.");

                    admin.Password= _adminPasswordHasher.HashPassword(admin, request.NewPassword);
                    await _adminRepository.UpdateAdminAsync(admin);
                    email = admin.Email;
                    fullName = admin.FullName;
                    break;

                case "Doctor":
                    var doctor = await _doctorRepository.GetDoctorByIdAsync(userId);
                    if (doctor == null) throw new KeyNotFoundException("Doctor not found.");

                    if (_doctorPasswordHasher.VerifyHashedPassword(doctor, doctor.Password, request.CurrentPassword) == PasswordVerificationResult.Failed)
                        throw new UnauthorizedAccessException("Current password is incorrect.");

                    doctor.Password = _doctorPasswordHasher.HashPassword(doctor, request.NewPassword);
                    await _doctorRepository.UpdateDoctorAsync(doctor);
                    email = doctor.Email;
                    fullName = doctor.FullName;
                    break;

                case "CustomerService":
                    var customerService = await _customerServiceRepository.GetCustomerServiceByIdAsync(userId);
                    if (customerService == null) throw new KeyNotFoundException("Customer service user not found.");

                    if (_customerServicePasswordHasher.VerifyHashedPassword(customerService, customerService.Password, request.CurrentPassword) == PasswordVerificationResult.Failed)
                        throw new UnauthorizedAccessException("Current password is incorrect.");

                    customerService.Password = _customerServicePasswordHasher.HashPassword(customerService, request.NewPassword);
                    await _customerServiceRepository.UpdateCustomerServiceAsync(customerService);
                    email = customerService.Email;
                    fullName = customerService.FullName;
                    break;

                default:
                    throw new UnauthorizedAccessException("Invalid role.");
            }

            await _emailService.SendEmailAsync(email, "Password Change Notification",
                $"Dear {fullName},\n\n" +
                "Your password has been successfully changed. If you did not request this change, please contact support immediately.\n\n" +
                "Best regards,\nDental Clinic Team");

            return new ChangePasswordResponse
            {
                Success = true,
                WarningMessage = "Your password has been updated. Please log in again with your new credentials.",
                RequireReLogin = true
            };
        }
    }

}
