namespace DentalClinicManagement.DomainLayer.Entities
{
    public class Payment : EntityBase
    {
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } = string.Empty; // e.g. Cash, Cheque, Insurance
        public string Notes { get; set; } = string.Empty;

        // Relationships
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }

        public Guid? CustomerServiceId { get; set; } // who recorded it
        public CustomerService? CustomerService { get; set; }
        public ICollection<Cheque> Cheques { get; set; } = new List<Cheque>();
        public ICollection<Insurance> Insurances { get; set; } = new List<Insurance>();
    }
}
