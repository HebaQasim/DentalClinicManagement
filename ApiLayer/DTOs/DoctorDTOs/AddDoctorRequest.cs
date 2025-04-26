namespace DentalClinicManagement.ApiLayer.DTOs.DoctorDTOs
{
    public class AddDoctorRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string WorkingTime { get; init; }=String.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string ColorCode {  get; set; } = string.Empty;
    }
}
