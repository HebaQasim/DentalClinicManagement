using MediatR;

namespace DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.AddCustomerService
{
    public class AddCustomerServiceCommand : IRequest<Guid>
    {
        public string FullName { get; init; }
        public string WorkingTime { get; init; }
        public string Email { get; init; }
        public string PhoneNumber { get; init; }
    }
}
