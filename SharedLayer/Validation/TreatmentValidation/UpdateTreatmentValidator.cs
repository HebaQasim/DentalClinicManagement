using DentalClinicManagement.ApplicationLayer.TreatmentFeatures.UpdateTreatment;
using FluentValidation;

namespace DentalClinicManagement.SharedLayer.Validation.TreatmentValidation
{

    public class UpdateTreatmentValidator : AbstractValidator<UpdateTreatmentCommand>
    {
        public UpdateTreatmentValidator()
        {
            RuleFor(t => t.Name)
                 .MaximumLength(100)
                 .When(t => !string.IsNullOrEmpty(t.Name));

            RuleFor(t => t.Category)
                .MaximumLength(100)
                .When(t => !string.IsNullOrEmpty(t.Category));

            RuleFor(t => t.Price)
        .NotNull()
        .GreaterThan(0)
        .When(t => t.Price.HasValue);


        }
    }

}
