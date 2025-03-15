using AutoMapper;
using DentalClinicManagement.ApiLayer.DTOs.CustomerServiceDTOs;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.InfrastructureLayer.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.GetCustomerService
{
    public class GetAllCustomerServicesCommandHandler : IRequestHandler<GetAllCustomerServicesCommand, List<GetCustomerServiceDto>>
    {

        private readonly IMapper _mapper;
        private readonly ICustomerServiceRepository _repository;

        public GetAllCustomerServicesCommandHandler(DentalClinicDbContext context, IMapper mapper, ICustomerServiceRepository repository)
        {
            _repository = repository;

            _mapper = mapper;
        }

        public async Task<List<GetCustomerServiceDto>> Handle(GetAllCustomerServicesCommand request, CancellationToken cancellationToken)
        {
            var customerServices = await _repository.GetAllCustomerServicesAsync();
            return _mapper.Map<List<GetCustomerServiceDto>>(customerServices);
        }
    }
}
