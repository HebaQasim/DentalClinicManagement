using DentalClinicManagement.ApiLayer.DTOs.Common;

namespace DentalClinicManagement.ApiLayer.DTOs.DoctorDTOs
{
    public class DoctorsGetRequest : ResourcesQueryRequest
    {
        public string? SearchTerm { get; init; }
    }
}
