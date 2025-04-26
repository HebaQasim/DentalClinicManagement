namespace DentalClinicManagement.DomainLayer.Interfaces.IServices
{
    public interface IUserContext
    {
        Guid Id { get; }

        string Role { get; }

        string Email { get; }
    }
}
