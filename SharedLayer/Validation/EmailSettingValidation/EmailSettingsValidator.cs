namespace DentalClinicManagement.SharedLayer.Validation.EmailSettingValidation
{
    using FluentValidation;

    public class EmailSettingsValidator : AbstractValidator<EmailSettings>
    {
        public EmailSettingsValidator()
        {
            RuleFor(x => x.SmtpServer).NotEmpty().WithMessage("SMTP Server is required.");
            RuleFor(x => x.Port).GreaterThan(0).WithMessage("SMTP Port must be valid.");
            RuleFor(x => x.SenderEmail).NotEmpty().WithMessage("SMTP User is required.");
            RuleFor(x => x.SenderPassword).NotEmpty().WithMessage("SMTP Password is required.");
        }
    }

}
