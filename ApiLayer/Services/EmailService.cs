using DentalClinicManagement.DomainLayer.Interfaces.IServices;
using DentalClinicManagement.SharedLayer.Validation.EmailSettingValidation;
using FluentValidation;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace DentalClinicManagement.ApiLayer.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings, IValidator<EmailSettings> validator)
        {
            _emailSettings = emailSettings.Value;


            var validationResult = validator.Validate(_emailSettings);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using var client = new SmtpClient(_emailSettings.SmtpServer)
            {
                Port = _emailSettings.Port,
                Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.SenderPassword),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.SenderEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };

            mailMessage.To.Add(to);

            await client.SendMailAsync(mailMessage);
        }
    }
}

