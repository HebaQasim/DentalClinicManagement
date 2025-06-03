using DentalClinicManagement.ApiLayer.DTOs.DoctorDTOs;
using MediatR;

namespace DentalClinicManagement.ApplicationLayer.DoctorFeatures.SearchDoctor
{
    public class GetDoctorByNameOrPhoneCommand : IRequest<List<GetDoctorDto>>
    {
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
