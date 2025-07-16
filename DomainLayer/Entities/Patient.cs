namespace DentalClinicManagement.DomainLayer.Entities
{
    public class Patient : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public DateTime DOB { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string EmergencyContact { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Job { get; set; } = string.Empty;
        // 🔗 Registered by Customer Service
        public Guid CustomerServiceId { get; set; }
        public CustomerService CustomerService { get; set; }
        // 🔗 Many-to-Many with Doctors
        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();

    }
}
