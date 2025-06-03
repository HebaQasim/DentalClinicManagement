using DentalClinicManagement.ApiLayer.DTOs.TreatmentDTOs;
using MediatR;

namespace DentalClinicManagement.ApplicationLayer.TreatmentFeatures.SearchTreatment
{
    public class SearchTreatmentCommand : IRequest<List<GetTreatmentDto>>
    {
        public string? Name { get; set; }
    }
}
