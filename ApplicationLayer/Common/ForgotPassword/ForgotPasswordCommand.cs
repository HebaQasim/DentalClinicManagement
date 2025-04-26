using MediatR;

namespace DentalClinicManagement.ApplicationLayer.Common.ForgotPassword
{
    public class ForgotPasswordCommand : IRequest
    {
        public string Email { get; set; } = string.Empty;
    }
}
