﻿namespace DentalClinicManagement.DomainLayer.Interfaces.IServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
