using AutoMapper;
using DentalClinicManagement.ApiLayer.DTOs.TreatmentDTOs;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.DomainLayer.Interfaces.IServices;
using DentalClinicManagement.DomainLayer.Models;
using DentalClinicManagement.InfrastructureLayer.Repositories;
using MediatR;
using System.Security.Authentication;

namespace DentalClinicManagement.ApplicationLayer.TreatmentFeatures.GetTreatment.GetTreatmentById
{
    public class GetTreatmentByIdCommandHandler : IRequestHandler<GetTreatmentByIdCommand, GetTreatmentDto>
    {
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly IMapper _mapper;
        private readonly IAdminRepository _adminRepository;
        private readonly IUserContext _userContext;

        public GetTreatmentByIdCommandHandler(ITreatmentRepository treatmentRepository, IMapper mapper, IAdminRepository adminRepository,
            IUserContext userContext)
        {
            _treatmentRepository = treatmentRepository;
            _mapper = mapper;
            _adminRepository = adminRepository;
            _userContext = userContext;
        }

        public async Task<GetTreatmentDto> Handle(GetTreatmentByIdCommand request, CancellationToken cancellationToken)
        {
            if (!await _adminRepository.ExistsByIdAsync(_userContext.Id, cancellationToken))
                throw new KeyNotFoundException("Admin not found.");

            if (_userContext.Role != UserRoles.Admin)
                throw new AuthenticationException("Only admin can get treatment by ID.");
            var treatment = await _treatmentRepository.GetTreatmentByIdAsync(request.Id, cancellationToken);
            if (treatment == null)
                throw new KeyNotFoundException($"Treatment with ID {request.Id} not found.");

            return _mapper.Map<GetTreatmentDto>(treatment);
        }
    }
}
