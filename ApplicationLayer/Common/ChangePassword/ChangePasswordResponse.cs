namespace DentalClinicManagement.ApplicationLayer.Common.ChangePassword
{
    public class ChangePasswordResponse
    {
        public bool Success { get; set; }
        public string? WarningMessage { get; set; }
        public bool RequireReLogin { get; set; }
    }
}
