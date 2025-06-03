using AutoMapper;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.DomainLayer.Interfaces.IServices;
using DentalClinicManagement.DomainLayer.Models;
using DentalClinicManagement.InfrastructureLayer.Repositories;
using FluentValidation;
using MediatR;
using System;
using System.Security.Authentication;

namespace DentalClinicManagement.ApplicationLayer.TreatmentFeatures.UpdateTreatment
{

    public class UpdateTreatmentCommandHandler : IRequestHandler<UpdateTreatmentCommand, bool>
    {
        private readonly ITreatmentRepository _repository;
        private readonly IValidator<UpdateTreatmentCommand> _validator;
        private readonly IMapper _mapper;
        private readonly IAdminRepository _adminRepository;
        private readonly IUserContext _userContext;
        public UpdateTreatmentCommandHandler(ITreatmentRepository repository, IValidator<UpdateTreatmentCommand> validator, IMapper mapper, IAdminRepository adminRepository,
            IUserContext userContext)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
            _adminRepository = adminRepository;
            _userContext = userContext;
        }

        public async Task<bool> Handle(UpdateTreatmentCommand request, CancellationToken cancellationToken)
        {
            if (!await _adminRepository.ExistsByIdAsync(_userContext.Id, cancellationToken))
                throw new KeyNotFoundException("Admin not found.");

            if (_userContext.Role != UserRoles.Admin)
                throw new AuthenticationException("Only admin can update treatments.");
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var treatment = await _repository.GetTreatmentByIdAsync(request.Id, cancellationToken);
            if (treatment is null)
                return false;

            if (!string.IsNullOrEmpty(request.Name))
                treatment.Name = request.Name;

            if (!string.IsNullOrEmpty(request.Category))
                treatment.Category = request.Category;

            if (request.Price.HasValue)
                treatment.Price = request.Price.Value;
            await _repository.UpdateTreatmentAsync(treatment);

            return true;
        }

    }
}
