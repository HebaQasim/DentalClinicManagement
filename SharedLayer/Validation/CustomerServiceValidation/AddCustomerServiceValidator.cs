using FluentValidation;
using Microsoft.EntityFrameworkCore;
using DentalClinicManagement.InfrastructureLayer.DbContexts;
using DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.AddCustomerService;

namespace DentalClinicManagement.SharedLayer.Validation.CustomerServiceValidation
{
    public class AddCustomerServiceValidator : AbstractValidator<AddCustomerServiceCommand>
    {
        private readonly DentalClinicDbContext _context;

        public AddCustomerServiceValidator(DentalClinicDbContext context)
        {
            _context = context;

            RuleFor(cs => cs.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(cs => cs.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(cs => cs.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MustAsync(async (email, cancellationToken) =>
                    !await _context.CustomerServices.AnyAsync(cs => cs.Email == email, cancellationToken))
                .WithMessage("This email is already in use.");

            RuleFor(cs => cs.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .MaximumLength(20).WithMessage("Phone number cannot exceed 20 characters.");
        }
    }
}

