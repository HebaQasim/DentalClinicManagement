using AutoMapper;
using DentalClinicManagement.ApiLayer.DTOs.CustomerServiceDTOs;
using DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.AddCustomerService;
using DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.UpdateCustomerService;
using DentalClinicManagement.DomainLayer.Entities;

namespace DentalClinicManagement.ApplicationLayer.Mapping
{
    public class CustomerServiceProfile : Profile
    {
        public CustomerServiceProfile()
        {
            CreateMap<AddCustomerServiceRequest, AddCustomerServiceCommand>();
            CreateMap<AddCustomerServiceCommand, CustomerService>();
            CreateMap<CustomerService, GetCustomerServiceDto>();
            CreateMap<UpdateCustomerServiceCommand, CustomerService>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        }


    }
}
