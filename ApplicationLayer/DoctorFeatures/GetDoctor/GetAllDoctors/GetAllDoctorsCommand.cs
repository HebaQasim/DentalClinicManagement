using DentalClinicManagement.ApiLayer.DTOs.DoctorDTOs;
using DentalClinicManagement.DomainLayer.Interfaces.IServices;
using DentalClinicManagement.InfrastructureLayer.Repositories;
using MediatR;

namespace DentalClinicManagement.ApplicationLayer.DoctorFeatures.GetDoctor.GetAllDoctors
{
    public class GetAllDoctorsCommand : IRequest<PaginatedList<GetDoctorDto>>, IPaginationParams
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
