using DentalClinicManagement.DomainLayer.Entities;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.InfrastructureLayer.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace DentalClinicManagement.InfrastructureLayer.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DentalClinicDbContext _context;

        public AdminRepository(DentalClinicDbContext context)
        {
            _context = context;
        }

        public async Task<Admin?> GetAdminByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Admins
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public async Task UpdateAdminAsync(Admin admin)
        {
            _context.Admins.Update(admin);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistsByEmailAsync(string email,
     CancellationToken cancellationToken = default)
        {
            return await _context.Admins.AnyAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<bool> ExistsByIdAsync(Guid id,
          CancellationToken cancellationToken = default)
        {
            return await _context.Admins
              .AnyAsync(u => u.Id == id, cancellationToken);
        }
        public async Task<Admin?> GetByEmailAsync(string email)
        {
            return await _context.Admins
                .Include(a => a.Role) // مهم إذا كنت تحتاج معلومات الدور (Role)
                .FirstOrDefaultAsync(a => a.Email.ToLower() == email.ToLower());
        }


    }
}
