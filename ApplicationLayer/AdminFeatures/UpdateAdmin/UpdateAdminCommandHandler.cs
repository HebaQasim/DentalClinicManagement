using AutoMapper;
using DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.UpdateCustomerService;
using DentalClinicManagement.DomainLayer.Entities;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.DomainLayer.Interfaces.IServices;
using DentalClinicManagement.DomainLayer.Models;
using DentalClinicManagement.InfrastructureLayer.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Authentication;

namespace DentalClinicManagement.ApplicationLayer.AdminFeatures.UpdateAdmin
{
    public class UpdateAdminCommandHandler : IRequestHandler<UpdateAdminCommand, UpdateAdminResponse>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IPasswordHasher<Admin> _passwordHasher;
        private readonly IEmailService _emailService;
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateAdminCommand> _validator;
        private readonly IUserContext _userContext;

        public UpdateAdminCommandHandler(
               IAdminRepository adminRepository,
               IPasswordHasher<Admin> passwordHasher,
               IEmailService emailService,
               IPasswordGenerator passwordGenerator,
               IMapper mapper,
               IValidator<UpdateAdminCommand> validator,
               IUserContext userContext)
        {
            _adminRepository = adminRepository;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
            _passwordGenerator = passwordGenerator;
            _mapper = mapper;
            _validator = validator;
            _userContext = userContext;
        }
        public async Task<UpdateAdminResponse> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
        {
            if (!await _adminRepository.ExistsByIdAsync(_userContext.Id, cancellationToken))
            {
                throw new KeyNotFoundException($"Admin with ID {request.Id} not found.");
            }
            if (_userContext.Role != UserRoles.Admin)
            {
                throw new AuthenticationException("Access denied. Only an admin can update admin data.");
            }
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var admin = await _adminRepository.GetAdminByIdAsync(request.Id, cancellationToken);
            if (admin == null) return new UpdateAdminResponse { Success = false, WarningMessage = "Customer Service not found" };
            string? warningMessage = null;
            // var oldEmail = admin.Email;
            bool requireReLogin = false;
            // تحديث البيانات


            // إذا تم تغيير البريد الإلكتروني
            if (!string.IsNullOrEmpty(request.Email) && request.Email != admin.Email)
            {
                // توليد كلمة مرور جديدة
                var newPassword = await _passwordGenerator.GenerateUniquePasswordAsync();
                admin.Password = _passwordHasher.HashPassword(admin, newPassword);

                // إرسال البريد الإلكتروني الجديد
                await _emailService.SendEmailAsync(request.Email, "Your Email Has Been Updated",
                    $"Dear {admin.FullName},\n\n" +
                    "Your email has been updated successfully. Here are your new login credentials:\n\n" +
                    $"Email: {request.Email}\n" +
                    $"Password: {newPassword}\n\n" +
                    "Please change your password after logging in.\n\n" +
                    "Best regards,\nDental Clinic Team");
                warningMessage = "Your email has been updated. Please log in again with your new credentials.";
                requireReLogin = true;
            }
            //if (!string.IsNullOrEmpty(request.Password))
            //{
            //    admin.Password = _passwordHasher.HashPassword(admin, request.Password);
            //    await _emailService.SendEmailAsync(admin.Email, "Password Change Notification",
            //        $"Dear {admin.FullName},\n\n" +
            //        "Your password has been successfully changed. If you did not request this change, please contact support immediately.\n\n" +
            //        "Best regards,\nDental Clinic Team");
            //    warningMessage = "Your password has been updated. Please log in again with your new credentials.";
            //    requireReLogin = true;
            //}
            _mapper.Map(request, admin);
            await _adminRepository.UpdateAdminAsync(admin);

            return new UpdateAdminResponse
            {
                Success = true,
                WarningMessage = warningMessage,
                RequireReLogin = requireReLogin
            };
        }





    }
}
