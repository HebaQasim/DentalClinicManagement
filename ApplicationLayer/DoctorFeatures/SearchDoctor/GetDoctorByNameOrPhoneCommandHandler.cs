using AutoMapper;
using DentalClinicManagement.ApiLayer.DTOs.DoctorDTOs;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.DomainLayer.Interfaces.IServices;
using DentalClinicManagement.DomainLayer.Models;
using DentalClinicManagement.InfrastructureLayer.Repositories;
using MediatR;
using System.Security.Authentication;

namespace DentalClinicManagement.ApplicationLayer.DoctorFeatures.SearchDoctor
{
    public class GetDoctorByNameOrPhoneCommandHandler : IRequestHandler<GetDoctorByNameOrPhoneCommand, List<GetDoctorDto>>
    {
        private readonly IDoctorRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAdminRepository _adminRepository;
        private readonly IUserContext _userContext;

        public GetDoctorByNameOrPhoneCommandHandler(IDoctorRepository repository, IMapper mapper, IAdminRepository adminRepository, IUserContext userContext)
        {
            _repository = repository;
            _mapper = mapper;
            _adminRepository = adminRepository;
            _userContext = userContext;
        }

        public async Task<List<GetDoctorDto>> Handle(GetDoctorByNameOrPhoneCommand request, CancellationToken cancellationToken)
        {
            if (!await _adminRepository.ExistsByIdAsync(_userContext.Id, cancellationToken))
                throw new KeyNotFoundException("Admin not found.");

            if (_userContext.Role != UserRoles.Admin)
                throw new AuthenticationException("Only admin can search a doctor.");


            var doctors = await _repository.GetAllDoctorsAsync(cancellationToken);

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                var normalizedSearchName = request.Name.Replace(" ", "").ToLower();
                doctors = doctors.Where(d =>
                    !string.IsNullOrEmpty(d.FullName) &&
                    d.FullName.Replace(" ", "").ToLower().Contains(normalizedSearchName)
                ).ToList();
            }

            if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
            {
                doctors = doctors.Where(d =>
                    !string.IsNullOrEmpty(d.PhoneNumber) &&
                    d.PhoneNumber.Contains(request.PhoneNumber)
                ).ToList();
            }

            // استخدم AutoMapper لتحويل قائمة Doctor إلى قائمة GetDoctorDto
            var result = _mapper.Map<List<GetDoctorDto>>(doctors);
            return result;
        }
    }
}
