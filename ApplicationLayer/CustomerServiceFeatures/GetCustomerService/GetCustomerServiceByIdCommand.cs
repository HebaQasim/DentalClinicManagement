using DentalClinicManagement.ApiLayer.DTOs.CustomerServiceDTOs;
using MediatR;

namespace DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.GetCustomerService
{
    public class GetCustomerServiceByIdCommand : IRequest<GetCustomerServiceDto>
    {
        public Guid Id { get; }

        public GetCustomerServiceByIdCommand(Guid id)
        {
            Id = id;
        }
    }
}
