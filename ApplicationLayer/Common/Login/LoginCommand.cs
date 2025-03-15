using MediatR;

namespace DentalClinicManagement.ApplicationLayer.Common.Login
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public string Email { get; init; }
        public string Password { get; init; }
    }
}
