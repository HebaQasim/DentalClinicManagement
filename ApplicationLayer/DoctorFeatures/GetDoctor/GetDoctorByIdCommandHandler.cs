using AutoMapper;
using DentalClinicManagement.ApiLayer.DTOs.DoctorDTOs;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.DomainLayer.Interfaces.IServices;
using DentalClinicManagement.DomainLayer.Models;
using DentalClinicManagement.InfrastructureLayer.Repositories;
using MediatR;
using System.Security.Authentication;

namespace DentalClinicManagement.ApplicationLayer.DoctorFeatures.GetDoctor
{
    public class GetDoctorByIdCommandHandler : IRequestHandler<GetDoctorByIdCommand, GetDoctorDto>
    {
        private readonly IDoctorRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAdminRepository _adminRepository;
        private readonly IUserContext _userContext;

        public GetDoctorByIdCommandHandler(IDoctorRepository repository, IMapper mapper, IAdminRepository adminRepository, IUserContext userContext)
        {
            _repository = repository;
            _mapper = mapper;
            _adminRepository = adminRepository;
            _userContext = userContext;
        }

        public async Task<GetDoctorDto> Handle(GetDoctorByIdCommand request, CancellationToken cancellationToken)
        {
            if (!await _adminRepository.ExistsByIdAsync(_userContext.Id, cancellationToken))
                throw new KeyNotFoundException("Admin not found.");

            if (_userContext.Role != UserRoles.Admin)
                throw new AuthenticationException("Only admin can get doctor by ID.");
            var doctor = await _repository.GetDoctorByIdAsync(request.Id);

            if (doctor == null)
            {
                throw new KeyNotFoundException("Doctor not found.");
            }

            return _mapper.Map<GetDoctorDto>(doctor);
        }
    }
}
