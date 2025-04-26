using AutoMapper;
using DentalClinicManagement.ApiLayer.DTOs.CustomerServiceDTOs;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.DomainLayer.Interfaces.IServices;
using DentalClinicManagement.DomainLayer.Models;
using DentalClinicManagement.InfrastructureLayer.DbContexts;
using DentalClinicManagement.InfrastructureLayer.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;

namespace DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.GetCustomerService
{
    public class GetAllCustomerServicesCommandHandler : IRequestHandler<GetAllCustomerServicesCommand, List<GetCustomerServiceDto>>
    {

        private readonly IMapper _mapper;
        private readonly ICustomerServiceRepository _repository;
        private readonly IAdminRepository _adminRepository;
        private readonly IUserContext _userContext;

        public GetAllCustomerServicesCommandHandler(DentalClinicDbContext context, IMapper mapper, ICustomerServiceRepository repository, IAdminRepository adminRepository, IUserContext userContext)
        {
            _repository = repository;

            _mapper = mapper;
            _userContext = userContext;
            _adminRepository = adminRepository;
        }

        public async Task<List<GetCustomerServiceDto>> Handle(GetAllCustomerServicesCommand request, CancellationToken cancellationToken)
        {
            if (!await _adminRepository.ExistsByIdAsync(_userContext.Id, cancellationToken))
            {
                throw new KeyNotFoundException($"Admin not found.");
            }
            if (_userContext.Role != UserRoles.Admin)
            {
                throw new AuthenticationException("Access denied. Only an admin can update admin data.");
            }
            var customerServices = await _repository.GetAllCustomerServicesAsync();
            return _mapper.Map<List<GetCustomerServiceDto>>(customerServices);
        }
    }
}
