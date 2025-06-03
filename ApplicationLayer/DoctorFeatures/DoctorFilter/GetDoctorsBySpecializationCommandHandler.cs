using AutoMapper;
using DentalClinicManagement.ApiLayer.DTOs.DoctorDTOs;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.DomainLayer.Interfaces.IServices;
using DentalClinicManagement.DomainLayer.Models;
using DentalClinicManagement.InfrastructureLayer.Repositories;
using MediatR;
using System.Security.Authentication;

namespace DentalClinicManagement.ApplicationLayer.DoctorFeatures.DoctorFilter
{
    public class GetDoctorsBySpecializationCommandHandler : IRequestHandler<GetDoctorsBySpecializationCommand, List<GetDoctorDto>>
    {
        private readonly IDoctorRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAdminRepository _adminRepository;
        private readonly IUserContext _userContext;
        public GetDoctorsBySpecializationCommandHandler(IDoctorRepository repository, IMapper mapper, IAdminRepository adminRepository, IUserContext userContext)
        {
            _repository = repository;
            _mapper = mapper;
            _adminRepository = adminRepository;
            _userContext = userContext;
        }

        public async Task<List<GetDoctorDto>> Handle(GetDoctorsBySpecializationCommand request, CancellationToken cancellationToken)
        {
            if (!await _adminRepository.ExistsByIdAsync(_userContext.Id, cancellationToken))
            {
                throw new KeyNotFoundException($"Admin not found.");
            }
            if (_userContext.Role != UserRoles.Admin)
            {
                throw new AuthenticationException("Access denied. Only an admin can do this.");
            }
            var doctors = await _repository.GetAllDoctorsAsync(cancellationToken);

            if (!string.IsNullOrWhiteSpace(request.Specialization))
            {
                // إزالة جميع المسافات وتحويل الأحرف إلى صغيرة للتطابق التام
                var normalizedInput = new string(request.Specialization
                    .Where(c => !char.IsWhiteSpace(c)).ToArray())
                    .ToLower();

                doctors = doctors
                    .Where(d =>
                        !string.IsNullOrWhiteSpace(d.Specialization) &&
                        new string(d.Specialization
                            .Where(c => !char.IsWhiteSpace(c)).ToArray())
                            .ToLower() == normalizedInput)
                    .ToList();
            }

            return _mapper.Map<List<GetDoctorDto>>(doctors);
        }
    }
}
