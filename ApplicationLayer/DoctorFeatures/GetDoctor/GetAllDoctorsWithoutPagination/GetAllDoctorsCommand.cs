using DentalClinicManagement.ApiLayer.DTOs.DoctorDTOs;
using MediatR;

namespace DentalClinicManagement.ApplicationLayer.DoctorFeatures.GetDoctor.GetAllDoctorsWithoutPagination
{
    public class GetAllDoctorsCommand : IRequest<List<GetDoctorDto>>
    {
    }
}
