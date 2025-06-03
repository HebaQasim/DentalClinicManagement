using AutoMapper;
using DentalClinicManagement.ApiLayer.DTOs.TreatmentDTOs;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.DomainLayer.Interfaces.IServices;
using DentalClinicManagement.DomainLayer.Models;
using DentalClinicManagement.InfrastructureLayer.Repositories;
using MediatR;
using System.Security.Authentication;

namespace DentalClinicManagement.ApplicationLayer.TreatmentFeatures.GetTreatment.GetAllTreatments
{
    public class GetAllTreatmentsCommandHandler : IRequestHandler<GetAllTreatmentsQuery, List<GetTreatmentDto>>
    {
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly IMapper _mapper;
        private readonly IAdminRepository _adminRepository;
        private readonly IUserContext _userContext;

        public GetAllTreatmentsCommandHandler(ITreatmentRepository treatmentRepository, IMapper mapper, IAdminRepository adminRepository,
            IUserContext userContext)
        {
            _treatmentRepository = treatmentRepository;
            _mapper = mapper;
            _adminRepository = adminRepository;
            _userContext = userContext;
        }

        public async Task<List<GetTreatmentDto>> Handle(GetAllTreatmentsQuery request, CancellationToken cancellationToken)
        {
            if (!await _adminRepository.ExistsByIdAsync(_userContext.Id, cancellationToken))
                throw new KeyNotFoundException("Admin not found.");

            if (_userContext.Role != UserRoles.Admin)
                throw new AuthenticationException("Only admin can get treatments.");
            var treatments = await _treatmentRepository.GetAllTreatmentsAsync(cancellationToken);
            return _mapper.Map<List<GetTreatmentDto>>(treatments);
        }
    }
}
