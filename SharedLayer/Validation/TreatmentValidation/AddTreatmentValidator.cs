using DentalClinicManagement.ApplicationLayer.TreatmentFeatures.AddTreatment;
using FluentValidation;

namespace DentalClinicManagement.SharedLayer.Validation.TreatmentValidation
{
    public class AddTreatmentValidator : AbstractValidator<AddTreatmentCommand>
    {
        public AddTreatmentValidator()
        {
            RuleFor(t => t.Name)
                .NotEmpty().WithMessage("Treatment name is required.")
                .MaximumLength(100).WithMessage("Treatment name must not exceed 100 characters.");

            RuleFor(t => t.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(t => t.Category)
                .NotEmpty().WithMessage("Category is required.")
                .MaximumLength(100).WithMessage("Category must not exceed 100 characters.");

           
        }
    }
}
