using DentalClinicManagement.ApiLayer.DTOs.CustomerServiceDTOs;
using MediatR;

namespace DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.GetCustomerService
{
    public class GetAllCustomerServicesCommand : IRequest<List<GetCustomerServiceDto>>
    {
    }
}
