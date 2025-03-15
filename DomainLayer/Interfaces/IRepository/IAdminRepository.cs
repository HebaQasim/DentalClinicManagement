using DentalClinicManagement.DomainLayer.Entities;

namespace DentalClinicManagement.DomainLayer.Interfaces.IRepository
{
    public interface IAdminRepository
    {
        Task<Admin?> GetAdminByIdAsync(Guid id);
        Task UpdateAdminAsync(Admin admin);
    }
}
