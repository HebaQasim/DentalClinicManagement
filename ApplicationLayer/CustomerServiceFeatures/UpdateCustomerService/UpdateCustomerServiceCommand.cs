using MediatR;

namespace DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.UpdateCustomerService
{
    public class UpdateCustomerServiceCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? IsActive { get; set; }
    }
}
