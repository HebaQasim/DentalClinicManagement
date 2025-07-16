namespace DentalClinicManagement.DomainLayer.Entities
{
    public class Appointment : EntityBase
    {
        public DateTime Date { get; set; }          // موعد الحجز
        public TimeSpan Time { get; set; }          // الساعة
        public string? Notes { get; set; }          // ملاحظات (اختياري)

        // العلاقة مع Patient
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }

        // العلاقة مع Doctor
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        // العلاقة مع CustomerService
        public Guid CustomerServiceId { get; set; }
        public CustomerService CustomerService { get; set; }
    }
}
