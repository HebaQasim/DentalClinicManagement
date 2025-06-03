using DentalClinicManagement.ApiLayer.DTOs.DoctorDTOs;
using MediatR;

namespace DentalClinicManagement.ApplicationLayer.DoctorFeatures.DoctorFilter
{
    public class GetDoctorsBySpecializationCommand : IRequest<List<GetDoctorDto>>
    {
        public string? Specialization { get; set; }
    }
}
