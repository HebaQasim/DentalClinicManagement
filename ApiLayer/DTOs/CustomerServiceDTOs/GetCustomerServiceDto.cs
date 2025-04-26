namespace DentalClinicManagement.ApiLayer.DTOs.CustomerServiceDTOs
{
    public class GetCustomerServiceDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string WorkingTime { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }

    }
}
