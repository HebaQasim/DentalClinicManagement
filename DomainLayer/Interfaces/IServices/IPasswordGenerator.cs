namespace DentalClinicManagement.DomainLayer.Interfaces.IServices
{
    public interface IPasswordGenerator
    {
        Task<string> GenerateUniquePasswordAsync(int length = 10);
    }
}
