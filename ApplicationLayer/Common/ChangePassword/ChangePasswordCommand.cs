using MediatR;

namespace DentalClinicManagement.ApplicationLayer.Common.ChangePassword
{
    public class ChangePasswordCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public string UserType { get; set; } // Admin, Doctor, CustomerService
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
