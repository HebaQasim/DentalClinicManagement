using DentalClinicManagement.ApiLayer.DTOs.TreatmentDTOs;
using MediatR;

namespace DentalClinicManagement.ApplicationLayer.TreatmentFeatures.GetTreatment.GetAllTreatments
{
    public class GetAllTreatmentsQuery : IRequest<List<GetTreatmentDto>>
    {
    }
}
