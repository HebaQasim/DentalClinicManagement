using DentalClinicManagement.ApiLayer.DTOs.DoctorDTOs;
using MediatR;

namespace DentalClinicManagement.ApplicationLayer.DoctorFeatures.GetDoctor.GetAllDoctors
{
    public class GetDoctorByIdCommand : IRequest<GetDoctorDto>
    {

        public Guid Id { get; }

        public GetDoctorByIdCommand(Guid id)
        {
            Id = id;
        }

    }
}
