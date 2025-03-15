using DentalClinicManagement.ApplicationLayer.DoctorFeatures.AddDoctor;
using DentalClinicManagement.InfrastructureLayer.DbContexts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DentalClinicManagement.SharedLayer.Validation.DoctorValidation
{
    public class AddDoctorValidator : AbstractValidator<AddDoctorCommand>
    {
        private readonly DentalClinicDbContext _context;

        public AddDoctorValidator(DentalClinicDbContext context)
        {
            _context = context;

            RuleFor(d => d.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(d => d.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(d => d.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MustAsync(async (email, cancellationToken) =>
                    !await _context.Doctors.AnyAsync(d => d.Email == email, cancellationToken))
                .WithMessage("This email is already in use.");

            RuleFor(d => d.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .MaximumLength(20).WithMessage("Phone number cannot exceed 20 characters.");

            RuleFor(d => d.Specialization)
                .NotEmpty().WithMessage("Specialization is required.");
        }
    }
}
