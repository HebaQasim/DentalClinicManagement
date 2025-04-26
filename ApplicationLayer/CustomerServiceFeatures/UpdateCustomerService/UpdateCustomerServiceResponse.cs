namespace DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.UpdateCustomerService
{
    public class UpdateCustomerServiceResponse
    {
        public bool Success { get; set; }
        public string? WarningMessage { get; set; }
        public bool RequireReLogin { get; set; }
        public UpdateCustomerServiceResponse() { }
    }
}
