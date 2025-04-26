using DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.UpdateCustomerService;
using MediatR;

namespace DentalClinicManagement.ApplicationLayer.AdminFeatures.UpdateAdmin
{
    public class UpdateAdminCommand : IRequest<UpdateAdminResponse>
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        //public string Password { get; set; }
    }
}
