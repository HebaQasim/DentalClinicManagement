namespace DentalClinicManagement.DomainLayer.Interfaces.IServices
{
    public interface IDateTimeProvider
    {
        DateOnly GetCurrentDateUTC();

        DateTime GetCurrentDateTimeUTC();
    }
}
