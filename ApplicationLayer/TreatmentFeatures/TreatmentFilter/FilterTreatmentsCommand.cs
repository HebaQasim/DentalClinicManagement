using DentalClinicManagement.ApiLayer.DTOs.TreatmentDTOs;
using MediatR;

namespace DentalClinicManagement.ApplicationLayer.TreatmentFeatures.TreatmentFilter
{
    public class FilterTreatmentsCommand : IRequest<List<GetTreatmentDto>>
    {
        public string? Category { get; set; }
        public decimal? Price { get; set; }
    }
}
