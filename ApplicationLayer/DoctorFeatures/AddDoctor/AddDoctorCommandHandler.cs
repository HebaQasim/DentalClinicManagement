using AutoMapper;
using DentalClinicManagement.ApiLayer.Services;
using DentalClinicManagement.DomainLayer.Entities;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.DomainLayer.Interfaces.IServices;
using DentalClinicManagement.DomainLayer.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Authentication;

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
        private readonly DoctorColorProvider _doctorColorProvider;
        private readonly IAdminRepository _adminRepository;
        private readonly IUserContext _userContext;

        public AddDoctorCommandHandler(
            IDoctorRepository doctorRepository,
            IPasswordHasher<Doctor> passwordHasher,
            IEmailService emailService,
            IPasswordGenerator passwordGenerator,
            IMapper mapper,
            IValidator<AddDoctorCommand> validator,
            DoctorColorProvider doctorColorProvider, IAdminRepository adminRepository, IUserContext userContext)
        {
            _doctorRepository = doctorRepository;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
            _passwordGenerator = passwordGenerator;
            _mapper = mapper;
            _validator = validator;
            _doctorColorProvider = doctorColorProvider;
            _adminRepository = adminRepository;
            _userContext = userContext;
        }

        public async Task<Guid> Handle(AddDoctorCommand request, CancellationToken cancellationToken)
        {
            if (!await _adminRepository.ExistsByIdAsync(_userContext.Id, cancellationToken))
            {
                throw new KeyNotFoundException($"Admin not found.");
            }
            if (_userContext.Role != UserRoles.Admin)
            {
                throw new AuthenticationException("Access denied. Only an admin can update admin data.");
            }
            // Validate request
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            // Generate random password
            var password = await _passwordGenerator.GenerateUniquePasswordAsync();
            var usedColors = await _doctorRepository.GetAllDoctorColorsAsync();

            // ✅ التحقق من أن اللون غير مستخدم بالفعل
            if (usedColors.Contains(request.ColorCode))
            {
                throw new InvalidOperationException("The selected color is already in use. Please choose a different color.");
            }
            // Map to Doctor entity
            var doctor = _mapper.Map<Doctor>(request);
            doctor.Password = _passwordHasher.HashPassword(doctor, password);
            doctor.RoleId = await _doctorRepository.GetDoctorRoleId();
            doctor.ColorCode = request.ColorCode;


            // Save to repository
            await _doctorRepository.AddDoctorAsync(doctor, cancellationToken);

            // Send email
            await _emailService.SendEmailAsync(request.Email, "Welcome to Dental Clinic",
                $"Dear {request.FullName}, \n\n" +
                "We are delighted to welcome you to the Dental Clinic family! " +
                "Your Doctor account has been successfully created, and you can now log in using the details below:\n\n" +
                $"Email: {request.Email}\n" +
                $"Password: {password}\n\n" +
                "If you have any questions, feel free to contact our support team.\n\n" +
                "Best regards,\n" +
                "Dental Clinic Team");

            return doctor.Id;
        }
    }
}
