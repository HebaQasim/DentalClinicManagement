using DentalClinicManagement.DomainLayer.Entities;
using System.Threading;

namespace DentalClinicManagement.DomainLayer.Interfaces.IRepository
{
    public interface IAdminRepository
    {
        Task<Admin?> GetAdminByIdAsync(Guid id, CancellationToken cancellationToken);
        Task UpdateAdminAsync(Admin admin);
        Task<bool> ExistsByEmailAsync(string email,
     CancellationToken cancellationToken = default);
        Task<bool> ExistsByIdAsync(Guid id,
          CancellationToken cancellationToken = default);
        Task<Admin?> GetByEmailAsync(string email);

    }
}
