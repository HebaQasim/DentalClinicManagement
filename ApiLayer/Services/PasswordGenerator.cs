using DentalClinicManagement.DomainLayer.Interfaces.IServices;
using DentalClinicManagement.InfrastructureLayer.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace DentalClinicManagement.ApiLayer.Services
{
    public class PasswordGenerator : IPasswordGenerator
    {
       

        public async Task<string> GenerateUniquePasswordAsync(int length = 10)
        {
            const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%&*";
            var random = new Random();

            string password;

            password = new string(Enumerable.Repeat(validChars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());



            return password;
        }
    }

}
