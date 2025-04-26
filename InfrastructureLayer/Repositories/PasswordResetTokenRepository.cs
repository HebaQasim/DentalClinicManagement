using DentalClinicManagement.DomainLayer.Entities;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.InfrastructureLayer.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace DentalClinicManagement.InfrastructureLayer.Repositories
{
    public class PasswordResetTokenRepository : IPasswordResetTokenRepository
    {
        private readonly DentalClinicDbContext _context;

        public PasswordResetTokenRepository(DentalClinicDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PasswordResetToken resetToken)
        {
            await _context.PasswordResetTokens.AddAsync(resetToken);
            await _context.SaveChangesAsync();
        }

        public async Task<PasswordResetToken?> GetByTokenAsync(string token)
        {
            return await _context.PasswordResetTokens
                .FirstOrDefaultAsync(t => t.Token == token);
        }

        public async Task UpdateAsync(PasswordResetToken resetToken)
        {
            _context.PasswordResetTokens.Update(resetToken);
            await _context.SaveChangesAsync();
        }
    }
}
