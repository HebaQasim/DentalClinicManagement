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
    }
}
