using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using MediatR;

namespace DentalClinicManagement.ApplicationLayer.TreatmentFeatures.DeleteTreatment
{
    public class DeleteTreatmentCommandHandler : IRequestHandler<DeleteTreatmentCommand, bool>
    {
        private readonly ITreatmentRepository _treatmentRepository;

        public DeleteTreatmentCommandHandler(ITreatmentRepository treatmentRepository)
        {
            _treatmentRepository = treatmentRepository;
        }

        public async Task<bool> Handle(DeleteTreatmentCommand request, CancellationToken cancellationToken)
        {
            var treatment = await _treatmentRepository.GetTreatmentByIdAsync(request.Id, cancellationToken);
            if (treatment is null)
                throw new KeyNotFoundException($"Treatment with ID {request.Id} not found.");

            await _treatmentRepository.DeleteTreatmentAsync(treatment, cancellationToken);
            return true;
        }
    }
}
