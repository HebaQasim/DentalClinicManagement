using AutoMapper;
using DentalClinicManagement.ApiLayer.DTOs.DoctorDTOs;
using DentalClinicManagement.ApiLayer.Services;
using DentalClinicManagement.ApplicationLayer.DoctorFeatures.GetDoctor.AddDoctorWithPaginationInTwoMethods;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.DomainLayer.Interfaces.IServices;
using DentalClinicManagement.DomainLayer.Models;
using DentalClinicManagement.InfrastructureLayer.Repositories;
using MediatR;
using System.Security.Authentication;

namespace DentalClinicManagement.ApplicationLayer.DoctorFeatures.GetDoctor.GetAllDoctorsWithoutPagination
{
    public class GetAllDoctorsCommandHandler : IRequestHandler<GetAllDoctorsCommand, List<GetDoctorDto>>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        private readonly IAdminRepository _adminRepository;
        private readonly IUserContext _userContext;

        public GetAllDoctorsCommandHandler(IDoctorRepository doctorRepository, IMapper mapper, IAdminRepository adminRepository, IUserContext userContext)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
            _adminRepository = adminRepository;
            _userContext = userContext;
        }

        public async Task<List<GetDoctorDto>> Handle(GetAllDoctorsCommand request, CancellationToken cancellationToken)
        {
            if (!await _adminRepository.ExistsByIdAsync(_userContext.Id, cancellationToken))
            {
                throw new KeyNotFoundException($"Admin not found.");
            }
            if (_userContext.Role != UserRoles.Admin)
            {
                throw new AuthenticationException("Access denied. Only an admin can update admin data.");
            }
            var doctors = await _doctorRepository.GetAllDoctorsAsync(cancellationToken);
            return _mapper.Map<List<GetDoctorDto>>(doctors);
        }
    }
}
