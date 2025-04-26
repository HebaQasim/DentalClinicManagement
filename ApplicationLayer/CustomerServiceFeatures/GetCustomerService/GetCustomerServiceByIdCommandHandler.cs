using AutoMapper;
using DentalClinicManagement.ApiLayer.DTOs.CustomerServiceDTOs;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.DomainLayer.Interfaces.IServices;
using DentalClinicManagement.DomainLayer.Models;
using MediatR;
using System.Security.Authentication;

namespace DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.GetCustomerService
{
    public class GetCustomerServiceByIdCommandHandler : IRequestHandler<GetCustomerServiceByIdCommand, GetCustomerServiceDto>
    {
        private readonly ICustomerServiceRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAdminRepository _adminRepository;
        private readonly IUserContext _userContext;

        public GetCustomerServiceByIdCommandHandler(ICustomerServiceRepository repository, IMapper mapper, IAdminRepository adminRepository, IUserContext userContext)
        {
            _repository = repository;
            _mapper = mapper;
            _adminRepository = adminRepository;
            _userContext = userContext;
        }

        public async Task<GetCustomerServiceDto> Handle(GetCustomerServiceByIdCommand request, CancellationToken cancellationToken)
        {
            if (!await _adminRepository.ExistsByIdAsync(_userContext.Id, cancellationToken))
            {
                throw new KeyNotFoundException($"Admin not found.");
            }
            if (_userContext.Role != UserRoles.Admin)
            {
                throw new AuthenticationException("Access denied. Only an admin can update admin data.");
            }
            var secretary = await _repository.GetCustomerServiceByIdAsync(request.Id);

            if (secretary == null)
            {
                throw new KeyNotFoundException("Secretary not found.");
            }

            return _mapper.Map<GetCustomerServiceDto>(secretary);
        }
    }
}
