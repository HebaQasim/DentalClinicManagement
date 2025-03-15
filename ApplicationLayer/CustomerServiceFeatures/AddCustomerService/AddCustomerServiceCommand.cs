using MediatR;

namespace DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.AddCustomerService
{
    public class AddCustomerServiceCommand : IRequest<Guid>
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string PhoneNumber { get; init; }
    }
}
