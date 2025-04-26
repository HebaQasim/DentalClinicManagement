using MediatR;

namespace DentalClinicManagement.ApplicationLayer.Common.ResetPassword
{
    public class ResetPasswordCommand : IRequest
    {
        public string Token { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
