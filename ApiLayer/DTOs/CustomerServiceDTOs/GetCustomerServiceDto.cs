namespace DentalClinicManagement.ApiLayer.DTOs.CustomerServiceDTOs
{
    public class GetCustomerServiceDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }

    }
}
