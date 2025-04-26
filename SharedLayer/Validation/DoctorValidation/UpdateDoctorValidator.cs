using DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.UpdateCustomerService;
using DentalClinicManagement.ApplicationLayer.DoctorFeatures.UpdateDoctor;
using FluentValidation;

namespace DentalClinicManagement.SharedLayer.Validation.DoctorValidation
{
    public class UpdateDoctorValidator : AbstractValidator<UpdateDoctorCommand>
    {
        public UpdateDoctorValidator()
        { 
            RuleFor(d => d.FullName).MaximumLength(100);
            RuleFor(d => d.WorkingTime).MaximumLength(300);
            RuleFor(d => d.Email).EmailAddress().When(cs => !string.IsNullOrEmpty(cs.Email));
            RuleFor(d => d.PhoneNumber).MaximumLength(20);
            RuleFor(d => d.Specialization).MaximumLength(100);
        }
    }
}
