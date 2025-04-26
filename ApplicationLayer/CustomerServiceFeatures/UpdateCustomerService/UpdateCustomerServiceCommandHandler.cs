using AutoMapper;
using DentalClinicManagement.ApiLayer.Services;
using DentalClinicManagement.DomainLayer.Entities;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.DomainLayer.Interfaces.IServices;
using DentalClinicManagement.DomainLayer.Models;
using DentalClinicManagement.InfrastructureLayer.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;

namespace DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.UpdateCustomerService
{
    public class UpdateCustomerServiceCommandHandler : IRequestHandler<UpdateCustomerServiceCommand, UpdateCustomerServiceResponse>
    {
        private readonly ICustomerServiceRepository _customerServiceRepository;
        private readonly IPasswordHasher<CustomerService> _passwordHasher;
        private readonly IEmailService _emailService;
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateCustomerServiceCommand> _validator;
        private readonly IAdminRepository _adminRepository;
        private readonly IUserContext _userContext;

        public UpdateCustomerServiceCommandHandler(
            ICustomerServiceRepository customerServiceRepository,
            IPasswordHasher<CustomerService> passwordHasher,
            IEmailService emailService,
            IPasswordGenerator passwordGenerator,
            IMapper mapper,
            IValidator<UpdateCustomerServiceCommand> validator, IAdminRepository adminRepository, IUserContext userContext)
        {
            _customerServiceRepository = customerServiceRepository;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
            _passwordGenerator = passwordGenerator;
            _mapper = mapper;
            _validator = validator;
            _adminRepository = adminRepository;
            _userContext = userContext;
        }

        public async Task<UpdateCustomerServiceResponse> Handle(UpdateCustomerServiceCommand request, CancellationToken cancellationToken)
        {
            if (!await _adminRepository.ExistsByIdAsync(_userContext.Id, cancellationToken))
            {
                throw new KeyNotFoundException($"Admin not found.");
            }
            if (_userContext.Role != UserRoles.Admin)
            {
                throw new AuthenticationException("Access denied. Only an admin can update admin data.");
            }
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var customerService = await _customerServiceRepository.GetCustomerServiceByIdAsync(request.Id);
            if (customerService == null) return new UpdateCustomerServiceResponse { Success = false, WarningMessage = "Customer Service not found" };

            bool wasActive = customerService.IsActive;
            bool isNowActive = request.IsActive ?? customerService.IsActive;
            bool emailChanged = !string.IsNullOrEmpty(request.Email) && request.Email != customerService.Email;
            bool shouldGeneratePassword = (!wasActive && isNowActive);
            bool requireReLogin = emailChanged || shouldGeneratePassword;
            string? warningMessage = null;

            // ⚠️ في حالة تحديث الإيميل بينما الحساب غير مفعل، نظهر تحذيرًا فقط
            if (emailChanged && !wasActive)
            {
                warningMessage = "The account is inactive. Please reactivate it if needed.";
            }

            string? newPassword = null;
            

            if (shouldGeneratePassword)
            {
                newPassword = await _passwordGenerator.GenerateUniquePasswordAsync();
                customerService.Password = _passwordHasher.HashPassword(customerService, newPassword);
                if (isNowActive)
                {
                    string recipientEmail = emailChanged ? request.Email : customerService.Email;
                    await _emailService.SendEmailAsync(recipientEmail, "Your new password",
                        $"Your new password is: {newPassword}");
                }
            }

            if (!isNowActive)
            {
                customerService.IsActive = false;
            }
            else if (!wasActive && isNowActive)
            {
                customerService.IsActive = true;
            }

            _mapper.Map(request, customerService);
            await _customerServiceRepository.UpdateCustomerServiceAsync(customerService);

            return new UpdateCustomerServiceResponse { Success = true, WarningMessage = warningMessage, RequireReLogin = requireReLogin };
        }
    }
}
