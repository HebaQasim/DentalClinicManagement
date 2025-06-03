using AutoMapper;
using DentalClinicManagement.ApiLayer.Services;
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

namespace DentalClinicManagement.ApplicationLayer.DoctorFeatures.UpdateDoctor
{
    public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, UpdateDoctorResponse>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPasswordHasher<Doctor> _passwordHasher;
        private readonly IEmailService _emailService;
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateDoctorCommand> _validator;
        private readonly DoctorColorProvider _doctorColorProvider;
        private readonly IAdminRepository _adminRepository;
        private readonly IUserContext _userContext;

        public UpdateDoctorCommandHandler(
            IDoctorRepository doctorRepository,
            IPasswordHasher<Doctor> passwordHasher,
            IEmailService emailService,
            IPasswordGenerator passwordGenerator,
            IMapper mapper,
            IValidator<UpdateDoctorCommand> validator,
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

        public async Task<UpdateDoctorResponse> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            if (!await _adminRepository.ExistsByIdAsync(_userContext.Id, cancellationToken))
            {
                throw new KeyNotFoundException($"Admin not found.");
            }
            if (_userContext.Role != UserRoles.Admin)
            {
                throw new AuthenticationException("Access denied. Only an admin can update doctor data.");
            }
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var doctor = await _doctorRepository.GetDoctorByIdAsync(request.Id);
            if (doctor == null) return new UpdateDoctorResponse { Success = false, WarningMessage = "Doctor not found" };

            bool wasActive = doctor.IsActive;
            bool isNowActive = request.IsActive ?? doctor.IsActive;
            bool emailChanged = !string.IsNullOrEmpty(request.Email) && request.Email != doctor.Email;
            bool shouldGeneratePassword = (!wasActive && isNowActive);
            bool requireReLogin = emailChanged || shouldGeneratePassword;
            string? warningMessage = null;

            if (emailChanged && !wasActive)
            {
                warningMessage = "The account is inactive. Please reactivate it if needed.";
            }

            string? newPassword = null;

            if (shouldGeneratePassword)
            {
                newPassword = await _passwordGenerator.GenerateUniquePasswordAsync();
                doctor.Password = _passwordHasher.HashPassword(doctor, newPassword);
                if (isNowActive)
                {
                    string recipientEmail = emailChanged ? request.Email : doctor.Email;
                    await _emailService.SendEmailAsync(recipientEmail, "Your new password", $"Your new password is: {newPassword}");
                }
            }

            if (!isNowActive)
            {
                doctor.IsActive = false;
            }
            else if (!wasActive && isNowActive)
            {
                doctor.IsActive = true;
            }

            if (!string.IsNullOrEmpty(request.ColorCode) && request.ColorCode != doctor.ColorCode)
            {
                var usedColors = await _doctorRepository.GetAllDoctorColorsAsync();
                if (usedColors.Contains(request.ColorCode))
                {
                    return new UpdateDoctorResponse { Success = false, WarningMessage = "Selected color is already in use." };
                }
            }

            _mapper.Map(request, doctor);
            await _doctorRepository.UpdateDoctorAsync(doctor);

            return new UpdateDoctorResponse { Success = true, WarningMessage = warningMessage, RequireReLogin = requireReLogin };
        }

    }
}
