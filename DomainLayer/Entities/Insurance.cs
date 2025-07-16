namespace DentalClinicManagement.DomainLayer.Entities
{
    public class Insurance : EntityBase
    {
        public Guid InsuranceId { get; set; }   // Primary Key
        public Guid PaymentId { get; set; }     // FK to Payment
        public string Provider { get; set; } = string.Empty;

        // 🔗 Navigation Property
        public Payment Payment { get; set; }
    }
}
