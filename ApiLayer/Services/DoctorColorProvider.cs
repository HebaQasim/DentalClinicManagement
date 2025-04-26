using DentalClinicManagement.DomainLayer.Models;
using Microsoft.Extensions.Options;

namespace DentalClinicManagement.ApiLayer.Services
{
    public class DoctorColorProvider
    {
        private readonly DoctorSettings _settings;

        public DoctorColorProvider(IOptions<DoctorSettings> settings)
        {
            _settings = settings.Value;
        }

       

        // إرجاع الألوان غير المستخدمة فقط
        public List<string> GetUnusedColors(IEnumerable<string> usedColors) =>
            _settings.AvailableColors.Except(usedColors).ToList();

        // اختيار لون عشوائي من الألوان غير المستخدمة
        public string GetRandomAvailableColor(IEnumerable<string> usedColors)
        {
            var unusedColors = GetUnusedColors(usedColors);
            if (!unusedColors.Any())
                throw new InvalidOperationException("No available colors left.");

            return unusedColors[new Random().Next(unusedColors.Count)];
        }
    }
}
