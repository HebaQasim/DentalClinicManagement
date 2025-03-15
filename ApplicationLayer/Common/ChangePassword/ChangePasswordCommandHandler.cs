using DentalClinicManagement.DomainLayer.Entities;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using MediatR;

using Microsoft.AspNetCore.Identity;

namespace DentalClinicManagement.ApplicationLayer.Common.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly ICustomerServiceRepository _customerServiceRepository;
        private readonly IPasswordHasher<Admin> _adminPasswordHasher;
        private readonly IPasswordHasher<Doctor> _doctorPasswordHasher;
        private readonly IPasswordHasher<CustomerService> _customerServicePasswordHasher;

        public ChangePasswordCommandHandler(
            IAdminRepository adminRepository,
            IDoctorRepository doctorRepository,
            ICustomerServiceRepository customerServiceRepository,
            IPasswordHasher<Admin> adminPasswordHasher,
            IPasswordHasher<Doctor> doctorPasswordHasher,
            IPasswordHasher<CustomerService> customerServicePasswordHasher)
        {
            _adminRepository = adminRepository;
            _doctorRepository = doctorRepository;
            _customerServiceRepository = customerServiceRepository;
            _adminPasswordHasher = adminPasswordHasher;
            _doctorPasswordHasher = doctorPasswordHasher;
            _customerServicePasswordHasher = customerServicePasswordHasher;
        }

        public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            switch (request.UserType)
            {
                case "Admin":
                    var admin = await _adminRepository.GetAdminByIdAsync(request.UserId);
                    if (admin == null) throw new KeyNotFoundException("Admin not found");

                    if (_adminPasswordHasher.VerifyHashedPassword(admin, admin.Password, request.CurrentPassword) == PasswordVerificationResult.Failed)
                        throw new UnauthorizedAccessException("Incorrect current password");

                    admin.Password = _adminPasswordHasher.HashPassword(admin, request.NewPassword);
                    await _adminRepository.UpdateAdminAsync(admin);
                    break;

                case "Doctor":
                    var doctor = await _doctorRepository.GetDoctorByIdAsync(request.UserId);
                    if (doctor == null) throw new KeyNotFoundException("Doctor not found");

                    if (_doctorPasswordHasher.VerifyHashedPassword(doctor, doctor.Password, request.CurrentPassword) == PasswordVerificationResult.Failed)
                        throw new UnauthorizedAccessException("Incorrect current password");

                    doctor.Password = _doctorPasswordHasher.HashPassword(doctor, request.NewPassword);
                    await _doctorRepository.UpdateDoctorAsync(doctor);
                    break;

                case "CustomerService":
                    var customerService = await _customerServiceRepository.GetCustomerServiceByIdAsync(request.UserId);
                    if (customerService == null) throw new KeyNotFoundException("Customer service user not found");

                    if (_customerServicePasswordHasher.VerifyHashedPassword(customerService, customerService.Password, request.CurrentPassword) == PasswordVerificationResult.Failed)
                        throw new UnauthorizedAccessException("Incorrect current password");

                    customerService.Password = _customerServicePasswordHasher.HashPassword(customerService, request.NewPassword);
                    await _customerServiceRepository.UpdateCustomerServiceAsync(customerService);
                    break;

                default:
                    throw new ArgumentException("Invalid user type");
            }

            return true;
        }
    }

}
