namespace DentalClinicManagement.ApiLayer.DTOs.TreatmentDTOs
{
    public class GetTreatmentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }
}
