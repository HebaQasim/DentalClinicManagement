using FluentValidation;

namespace DentalClinicManagement.Auth_Jwt
{


    public class JwtAuthConfigValidator : AbstractValidator<JwtAuthConfig>
    {
        public JwtAuthConfigValidator()
        {
            RuleFor(x => x.Key)
              .NotEmpty();

            RuleFor(x => x.Issuer)
              .NotEmpty();

            RuleFor(x => x.Audience)
              .NotEmpty();

            RuleFor(x => x.LifetimeMinutes)
              .NotEmpty()
              .GreaterThan(0);
        }

    }
}
