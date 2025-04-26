using FluentValidation;
using DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.UpdateCustomerService;

namespace DentalClinicManagement.SharedLayer.Validation.CustomerServiceValidation
{
    public class UpdateCustomerServiceValidator : AbstractValidator<UpdateCustomerServiceCommand>
    {
        public UpdateCustomerServiceValidator()
        {
            RuleFor(cs => cs.FullName).MaximumLength(100);
            RuleFor(cs => cs.WorkingTime).MaximumLength(300);
            RuleFor(cs => cs.Email).EmailAddress().When(cs => !string.IsNullOrEmpty(cs.Email));
            RuleFor(cs => cs.PhoneNumber).MaximumLength(20);
        }
    }
}
