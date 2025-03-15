namespace DentalClinicManagement.SharedLayer.Validation.EmailSettingValidation
{
    public class EmailSettings
    {
        public required string SmtpServer { get; set; }
        public required int Port { get; set; }
        public required string SenderEmail { get; set; }
        public required string SenderPassword { get; set; }
    }

}
