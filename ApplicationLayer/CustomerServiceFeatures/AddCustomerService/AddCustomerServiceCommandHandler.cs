using AutoMapper;
using DentalClinicManagement.DomainLayer.Entities;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.DomainLayer.Interfaces.IServices;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.AddCustomerService
{
    public class AddCustomerServiceCommandHandler : IRequestHandler<AddCustomerServiceCommand, Guid>
    {
        private readonly ICustomerServiceRepository _customerServiceRepository;
        private readonly IPasswordHasher<CustomerService> _passwordHasher;
        private readonly IEmailService _emailService;
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly IMapper _mapper;
        private readonly IValidator<AddCustomerServiceCommand> _validator;

        public AddCustomerServiceCommandHandler(
            ICustomerServiceRepository customerServiceRepository,
            IPasswordHasher<CustomerService> passwordHasher,
            IEmailService emailService,
            IPasswordGenerator passwordGenerator,
            IMapper mapper,
            IValidator<AddCustomerServiceCommand> validator)
        {
            _customerServiceRepository = customerServiceRepository;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
            _passwordGenerator = passwordGenerator;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Guid> Handle(AddCustomerServiceCommand request, CancellationToken cancellationToken)
        {
            // Validate request
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            // Generate random password
            var password = await _passwordGenerator.GenerateUniquePasswordAsync();

            // Map to CustomerService entity
            var customerService = _mapper.Map<CustomerService>(request);
            customerService.Password = _passwordHasher.HashPassword(customerService, password);
            customerService.RoleId = await _customerServiceRepository.GetCustomerServiceRoleId();

            // Save to repository
            await _customerServiceRepository.AddCustomerServiceAsync(customerService, cancellationToken);

            // Send email
            await _emailService.SendEmailAsync(request.Email, "Welcome to Dental Clinic",
                $"Dear {request.FirstName} {request.LastName},\n\n" +
                "We are delighted to welcome you to the Dental Clinic family! " +
                "Your account has been successfully created, and you can now log in using the details below:\n\n" +
                $"Email: {request.Email}\n" +
                $"Password: {password}\n\n" +
                "If you have any questions, feel free to contact our support team.\n\n" +
                "Best regards,\n" +
                "Dental Clinic Team");

            return customerService.Id;
        }
    }
}
