using DentalClinicManagement.ApiLayer.DTOs.DoctorDTOs;
using DentalClinicManagement.DomainLayer.Entities;
using DentalClinicManagement.DomainLayer.Models;
using DentalClinicManagement.InfrastructureLayer.Repositories;
using System.Threading.Tasks;

namespace DentalClinicManagement.DomainLayer.Interfaces.IRepository
{
    public interface IDoctorRepository
    {
        Task<Guid> GetDoctorRoleId();
        Task AddDoctorAsync(Doctor doctor, CancellationToken cancellationToken);
        // Task<PaginatedList<GetDoctorDto>> GetAllDoctorsAsync(int pageNumber, int pageSize);
        //Task<PaginatedList<Doctor>> GetDoctorAsync(Query<Doctor> query, CancellationToken cancellationToken);
        Task<Doctor?> GetDoctorByIdAsync(Guid id);
        Task UpdateDoctorAsync(Doctor doctor);
        Task<IEnumerable<string>> GetAllDoctorColorsAsync();
        Task<bool> ExistsByEmailAsync(string email,
     CancellationToken cancellationToken = default);
        Task<bool> ExistsByIdAsync(Guid id,
          CancellationToken cancellationToken = default);
        Task<List<Doctor>> GetAllDoctorsAsync(CancellationToken cancellationToken);
        Task<Doctor?> GetByEmailAsync(string email);

    }
}
