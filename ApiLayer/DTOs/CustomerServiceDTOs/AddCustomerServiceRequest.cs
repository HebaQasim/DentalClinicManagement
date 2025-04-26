namespace DentalClinicManagement.ApiLayer.DTOs.CustomerServiceDTOs
{
    public class AddCustomerServiceRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string WorkingTime { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
