using DentalClinicManagement.DbContexts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.UpdateCustomerService;

namespace DentalClinicManagement.SharedLayer.Validation.CustomerServiceValidation
{
    public class UpdateCustomerServiceValidator : AbstractValidator<UpdateCustomerServiceCommand>
    {
        public UpdateCustomerServiceValidator()
        {
            RuleFor(cs => cs.FirstName).MaximumLength(50);
            RuleFor(cs => cs.LastName).MaximumLength(50);
            RuleFor(cs => cs.Email).EmailAddress().When(cs => !string.IsNullOrEmpty(cs.Email));
            RuleFor(cs => cs.PhoneNumber).MaximumLength(20);
        }
    }
}
