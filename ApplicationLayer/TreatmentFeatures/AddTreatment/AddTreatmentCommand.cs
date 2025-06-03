using MediatR;

namespace DentalClinicManagement.ApplicationLayer.TreatmentFeatures.AddTreatment
{
    public class AddTreatmentCommand : IRequest<Guid>
    {
        public string Name { get; init; }
        public decimal Price { get; init; }
        public string Category { get; init; }

    }
}
