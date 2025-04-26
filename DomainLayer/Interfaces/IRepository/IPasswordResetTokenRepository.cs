using DentalClinicManagement.DomainLayer.Entities;

namespace DentalClinicManagement.DomainLayer.Interfaces.IRepository
{
    public interface IPasswordResetTokenRepository
    {
        Task AddAsync(PasswordResetToken resetToken);
        Task<PasswordResetToken?> GetByTokenAsync(string token);
        Task UpdateAsync(PasswordResetToken resetToken);
    }
}
