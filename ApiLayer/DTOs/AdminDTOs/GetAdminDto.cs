namespace DentalClinicManagement.ApiLayer.DTOs.AdminDTOs
{
    public class GetAdminDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
