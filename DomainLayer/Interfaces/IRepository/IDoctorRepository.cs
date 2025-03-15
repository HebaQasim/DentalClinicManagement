using DentalClinicManagement.ApiLayer.DTOs.DoctorDTOs;
using DentalClinicManagement.DomainLayer.Entities;
using DentalClinicManagement.InfrastructureLayer.Repositories;

namespace DentalClinicManagement.DomainLayer.Interfaces.IRepository
{
    public interface IDoctorRepository
    {
        Task<Guid> GetDoctorRoleId();
        Task AddDoctorAsync(Doctor doctor, CancellationToken cancellationToken);
        Task<PaginatedList<GetDoctorDto>> GetAllDoctorsAsync(int pageNumber, int pageSize);
        Task<Doctor?> GetDoctorByIdAsync(Guid id);
        Task UpdateDoctorAsync(Doctor doctor);
    }
}
