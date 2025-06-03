using MediatR;

namespace DentalClinicManagement.ApplicationLayer.TreatmentFeatures.DeleteTreatment
{
    public class DeleteTreatmentCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public DeleteTreatmentCommand(Guid id)
        {
            Id = id;
        }
    }
}
