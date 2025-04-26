using DentalClinicManagement.DomainLayer.Interfaces.IServices;

namespace DentalClinicManagement.ApiLayer.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetCurrentDateTimeUTC()
        {
            return DateTime.UtcNow;
        }

        public DateOnly GetCurrentDateUTC()
        {
            return DateOnly.FromDateTime(DateTime.UtcNow);
        }
    }
}
