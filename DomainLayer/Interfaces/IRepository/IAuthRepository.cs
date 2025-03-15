namespace DentalClinicManagement.DomainLayer.Interfaces.IRepository
{
    public interface IAuthRepository
    {
        Task<(object? User, string Role)?> AuthenticateAsync(
            string email, string password, CancellationToken cancellationToken = default);
    }
}
