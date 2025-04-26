using DentalClinicManagement.ApplicationLayer.AdminFeatures.UpdateAdmin;
using DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.UpdateCustomerService;
using FluentValidation;

namespace DentalClinicManagement.SharedLayer.Validation.AdminValidation
{
    public class UpdateAdminValidator : AbstractValidator<UpdateAdminCommand>
    {
        public UpdateAdminValidator() {
            RuleFor(a => a.FullName).MaximumLength(100);

            RuleFor(a => a.Email).EmailAddress().When(cs => !string.IsNullOrEmpty(cs.Email));
            RuleFor(a => a.PhoneNumber).MaximumLength(20);
        }
      
    }
}
