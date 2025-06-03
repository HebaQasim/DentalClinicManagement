using MediatR;

namespace DentalClinicManagement.ApplicationLayer.TreatmentFeatures.UpdateTreatment
{

    public class UpdateTreatmentCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string? Name { get; init; } = string.Empty;
        public string? Category { get; init; } = string.Empty;
        public decimal? Price { get; init; }
    }
}
