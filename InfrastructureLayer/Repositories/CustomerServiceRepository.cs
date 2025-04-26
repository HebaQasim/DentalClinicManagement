using DentalClinicManagement.DomainLayer.Entities;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.InfrastructureLayer.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace DentalClinicManagement.InfrastructureLayer.Repositories
{
    public class CustomerServiceRepository : ICustomerServiceRepository
    {
        private readonly DentalClinicDbContext _context;

        public CustomerServiceRepository(DentalClinicDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> GetCustomerServiceRoleId()
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "CustomerService");

            return role == null ? throw new InvalidOperationException("Customer Service role not found.") : role.Id;
        }
        public async Task<bool> ExistsByEmailAsync(string email,
     CancellationToken cancellationToken = default)
        {
            return await _context.CustomerServices.AnyAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<bool> ExistsByIdAsync(Guid id,
          CancellationToken cancellationToken = default)
        {
            return await _context.CustomerServices
              .AnyAsync(u => u.Id == id, cancellationToken);
        }

        public async Task AddCustomerServiceAsync(CustomerService customerService, CancellationToken cancellationToken)
        {
            await _context.CustomerServices.AddAsync(customerService, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<List<CustomerService>> GetAllCustomerServicesAsync()
        {
            return await _context.CustomerServices
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<CustomerService?> GetCustomerServiceByIdAsync(Guid id)
        {
            return await _context.CustomerServices
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task UpdateCustomerServiceAsync(CustomerService customerService)
        {
            _context.CustomerServices.Update(customerService);
            await _context.SaveChangesAsync();
        }
        public async Task<CustomerService?> GetByEmailAsync(string email)
        {
            return await _context.CustomerServices
                .Include(d => d.Role)
                .FirstOrDefaultAsync(d => d.Email.ToLower() == email.ToLower());
        }

    }
}
