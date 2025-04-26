using MediatR;

namespace DentalClinicManagement.ApplicationLayer.Common.ChangePassword
{
    public class ChangePasswordCommand : IRequest<ChangePasswordResponse>
    {
     
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
