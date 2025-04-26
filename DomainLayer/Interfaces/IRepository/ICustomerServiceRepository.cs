using DentalClinicManagement.DomainLayer.Entities;

namespace DentalClinicManagement.DomainLayer.Interfaces.IRepository
{
    public interface ICustomerServiceRepository
    {
        Task<Guid> GetCustomerServiceRoleId();
        Task AddCustomerServiceAsync(CustomerService customerService, CancellationToken cancellationToken);
        Task<List<CustomerService>> GetAllCustomerServicesAsync();
        Task<CustomerService?> GetCustomerServiceByIdAsync(Guid id);
        Task UpdateCustomerServiceAsync(CustomerService customerService);
        Task<bool> ExistsByEmailAsync(string email,
     CancellationToken cancellationToken = default);
        Task<bool> ExistsByIdAsync(Guid id,
          CancellationToken cancellationToken = default);
        Task<CustomerService?> GetByEmailAsync(string email);

    }
}
