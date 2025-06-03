namespace DentalClinicManagement.ApiLayer.DTOs.TreatmentDTOs
{
    public class UpdateTreatmentRequest
    {
        public string? Name { get; set; } = string.Empty;
        public string? Category { get; set; } = string.Empty;
        public decimal? Price { get; set; }
    }
}
