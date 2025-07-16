namespace DentalClinicManagement.DomainLayer.Entities
{
    public class Doctor : EntityBase
    {
        public string FullName { get; set; } = string.Empty;
        public string ColorCode { get; set; }= string.Empty;
        public string WorkingTime { get; set; }= string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public Guid RoleId { get; set; }
        public Role Role { get; set; }

        public ICollection<Patient> Patients { get; set; } = new List<Patient>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();


    }
}
