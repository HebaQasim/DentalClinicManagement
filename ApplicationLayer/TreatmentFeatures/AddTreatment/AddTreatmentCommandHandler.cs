using AutoMapper;
using DentalClinicManagement.DomainLayer.Entities;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.DomainLayer.Interfaces.IServices;
using DentalClinicManagement.DomainLayer.Models;
using FluentValidation;
using MediatR;
using System.Security.Authentication;

namespace DentalClinicManagement.ApplicationLayer.TreatmentFeatures.AddTreatment
{
    public class AddTreatmentCommandHandler : IRequestHandler<AddTreatmentCommand, Guid>
    {
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<AddTreatmentCommand> _validator;
        private readonly IAdminRepository _adminRepository;
        private readonly IUserContext _userContext;

        public AddTreatmentCommandHandler(
            ITreatmentRepository treatmentRepository,
            IMapper mapper,
            IValidator<AddTreatmentCommand> validator,
            IAdminRepository adminRepository,
            IUserContext userContext)
        {
            _treatmentRepository = treatmentRepository;
            _mapper = mapper;
            _validator = validator;
            _adminRepository = adminRepository;
            _userContext = userContext;
        }

        public async Task<Guid> Handle(AddTreatmentCommand request, CancellationToken cancellationToken)
        {
            if (!await _adminRepository.ExistsByIdAsync(_userContext.Id, cancellationToken))
                throw new KeyNotFoundException("Admin not found.");

            if (_userContext.Role != UserRoles.Admin)
                throw new AuthenticationException("Only admin can add treatments.");

            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var treatment = _mapper.Map<Treatment>(request);

            await _treatmentRepository.AddTreatmentAsync(treatment, cancellationToken);

            return treatment.Id;
        }
    }
}
