namespace DentalClinicManagement.ApiLayer.DTOs.TreatmentDTOs
{
    public class AddTreatmentRequest
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}
