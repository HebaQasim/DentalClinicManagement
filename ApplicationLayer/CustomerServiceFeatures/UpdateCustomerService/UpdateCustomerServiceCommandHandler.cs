using AutoMapper;
using DentalClinicManagement.DomainLayer.Entities;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.DomainLayer.Interfaces.IServices;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.UpdateCustomerService
{
    public class UpdateCustomerServiceCommandHandler : IRequestHandler<UpdateCustomerServiceCommand, bool>
    {
        private readonly ICustomerServiceRepository _customerServiceRepository;
        private readonly IPasswordHasher<CustomerService> _passwordHasher;
        private readonly IEmailService _emailService;
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateCustomerServiceCommand> _validator;

        public UpdateCustomerServiceCommandHandler(
            ICustomerServiceRepository customerServiceRepository,
            IPasswordHasher<CustomerService> passwordHasher,
            IEmailService emailService,
            IPasswordGenerator passwordGenerator,
            IMapper mapper,
            IValidator<UpdateCustomerServiceCommand> validator)
        {
            _customerServiceRepository = customerServiceRepository;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
            _passwordGenerator = passwordGenerator;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<bool> Handle(UpdateCustomerServiceCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var customerService = await _customerServiceRepository.GetCustomerServiceByIdAsync(request.Id);
            if (customerService == null) return false;

            string? newPassword = null;
            if (!string.IsNullOrEmpty(request.Email) && request.Email != customerService.Email)
            {
                newPassword = await _passwordGenerator.GenerateUniquePasswordAsync();
                customerService.Password = _passwordHasher.HashPassword(customerService, newPassword);
                await _emailService.SendEmailAsync(request.Email, "Your email has been updated",
                    $"Your new password is: {newPassword}");
            }

            _mapper.Map(request, customerService);
            await _customerServiceRepository.UpdateCustomerServiceAsync(customerService);
            return true;
        }
    }
}
