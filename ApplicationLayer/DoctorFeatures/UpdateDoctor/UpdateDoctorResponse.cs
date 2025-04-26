namespace DentalClinicManagement.ApplicationLayer.DoctorFeatures.UpdateDoctor
{
    public class UpdateDoctorResponse
    {
        public bool Success { get; set; }
        public string? WarningMessage { get; set; }
        public bool RequireReLogin { get; set; }
    }
}
