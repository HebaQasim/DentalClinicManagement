namespace DentalClinicManagement.DomainLayer.Entities
{
    public class Cheque:EntityBase
    {
        
        public Guid PaymentId { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public string BankName { get; set; } = string.Empty;

        // Navigation property
        public Payment Payment { get; set; }
    }
}
