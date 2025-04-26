namespace DentalClinicManagement.ApiLayer.DTOs.DoctorDTOs
{
    public class GetDoctorDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string ColorCode { get; set; }
        public string WorkingTime { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Specialization { get; set; }
        public bool IsActive { get; set; }
    }
}
