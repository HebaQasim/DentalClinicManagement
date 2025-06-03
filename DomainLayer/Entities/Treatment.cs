namespace DentalClinicManagement.DomainLayer.Entities
{
    public class Treatment:EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}
