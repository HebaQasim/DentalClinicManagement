using AutoMapper;
using DentalClinicManagement.DomainLayer.Entities;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.DomainLayer.Interfaces.IServices;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DentalClinicManagement.ApplicationLayer.DoctorFeatures.AddDoctor
{
    public class AddDoctorCommandHandler : IRequestHandler<AddDoctorCommand, Guid>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPasswordHasher<Doctor> _passwordHasher;
        private readonly IEmailService _emailService;
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly IMapper _mapper;
        private readonly IValidator<AddDoctorCommand> _validator;

        public AddDoctorCommandHandler(
            IDoctorRepository doctorRepository,
            IPasswordHasher<Doctor> passwordHasher,
            IEmailService emailService,
            IPasswordGenerator passwordGenerator,
            IMapper mapper,
            IValidator<AddDoctorCommand> validator)
        {
            _doctorRepository = doctorRepository;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
            _passwordGenerator = passwordGenerator;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Guid> Handle(AddDoctorCommand request, CancellationToken cancellationToken)
        {
            // Validate request
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            // Generate random password
            var password = await _passwordGenerator.GenerateUniquePasswordAsync();

            // Map to Doctor entity
            var doctor = _mapper.Map<Doctor>(request);
            doctor.Password = _passwordHasher.HashPassword(doctor, password);
            doctor.RoleId = await _doctorRepository.GetDoctorRoleId();

            // Save to repository
            await _doctorRepository.AddDoctorAsync(doctor, cancellationToken);

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

            return doctor.Id;
        }
    }
}
