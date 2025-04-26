namespace DentalClinicManagement.DomainLayer.Entities
{
    public class CustomerService : EntityBase
    {
        public string FullName { get; set; } = string.Empty;
        public string WorkingTime { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
