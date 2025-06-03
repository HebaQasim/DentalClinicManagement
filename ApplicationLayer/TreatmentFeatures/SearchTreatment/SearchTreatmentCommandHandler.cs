using AutoMapper;
using DentalClinicManagement.ApiLayer.DTOs.TreatmentDTOs;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.DomainLayer.Interfaces.IServices;
using DentalClinicManagement.DomainLayer.Models;
using DentalClinicManagement.InfrastructureLayer.Repositories;
using MediatR;
using System.Security.Authentication;

namespace DentalClinicManagement.ApplicationLayer.TreatmentFeatures.SearchTreatment
{
    public class SearchTreatmentCommandHandler : IRequestHandler<SearchTreatmentCommand, List<GetTreatmentDto>>
    {
        private readonly ITreatmentRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAdminRepository _adminRepository;
        private readonly IUserContext _userContext;

        public SearchTreatmentCommandHandler(ITreatmentRepository repository, IMapper mapper, IAdminRepository adminRepository, IUserContext userContext)
        {
            _repository = repository;
            _mapper = mapper;
            _adminRepository = adminRepository;
            _userContext = userContext;
        }

        public async Task<List<GetTreatmentDto>> Handle(SearchTreatmentCommand request, CancellationToken cancellationToken)
        {
            if (!await _adminRepository.ExistsByIdAsync(_userContext.Id, cancellationToken))
                throw new KeyNotFoundException("Admin not found.");

            if (_userContext.Role != UserRoles.Admin)
                throw new AuthenticationException("Only admin can search a treatment.");
            var treatments = await _repository.GetAllTreatmentsAsync(cancellationToken);

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                var normalizedSearchName = request.Name.Replace(" ", "").ToLower();

                treatments = treatments
                    .Where(t =>
                        !string.IsNullOrEmpty(t.Name) &&
                        t.Name.Replace(" ", "").ToLower().Contains(normalizedSearchName))
                    .ToList();
            }

            return _mapper.Map<List<GetTreatmentDto>>(treatments);
        }
    }
}
