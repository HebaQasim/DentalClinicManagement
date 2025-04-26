namespace DentalClinicManagement.ApplicationLayer.AdminFeatures.UpdateAdmin
{
    public class UpdateAdminResponse
    {
        public bool Success { get; set; }
        public string? WarningMessage { get; set; }
        public bool RequireReLogin { get; set; }
    }
}
