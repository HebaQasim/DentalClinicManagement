using AutoMapper;
using DentalClinicManagement.ApiLayer.DTOs.AdminDTOs;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.DomainLayer.Interfaces.IServices;
using DentalClinicManagement.InfrastructureLayer.Repositories;
using MediatR;

namespace DentalClinicManagement.ApplicationLayer.AdminFeatures.GetAdmin
{
    public class GetAdminByIdCommandHandler : IRequestHandler<GetAdminByIdCommand, GetAdminDto>
    {
      
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;
        private readonly IAdminRepository _adminRepository;

        public GetAdminByIdCommandHandler(IAdminRepository adminRepository, IMapper mapper, IUserContext userContext)
        {
           
            _mapper = mapper;
            _userContext = userContext;
            _adminRepository = adminRepository;
        }

        public async Task<GetAdminDto> Handle(GetAdminByIdCommand request, CancellationToken cancellationToken)
        {
            var adminId = _userContext.Id;
            var admin = await _adminRepository.GetAdminByIdAsync(adminId, cancellationToken)
                ?? throw new UnauthorizedAccessException("Admin not found.");

            return _mapper.Map<GetAdminDto>(admin);
        }
    }
}
