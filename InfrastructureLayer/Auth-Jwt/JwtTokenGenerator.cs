using DentalClinicManagement.DomainLayer.Entities;
using DentalClinicManagement.DomainLayer.Interfaces.IAuth;
using DentalClinicManagement.DomainLayer.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DentalClinicManagement.Auth_Jwt
{
    public class JwtTokenGenerator(IOptions<JwtAuthConfig> config) : IJwtTokenGenerator
    {
        private readonly JwtAuthConfig _config = config.Value;

        public JwtToken Generate(object user, string role)
        {
            var claims = new List<Claim>
        {
            new(ClaimTypes.Email, GetUserEmail(user)),
            new(ClaimTypes.Role, role)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _config.Issuer,
                audience: _config.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );
            var token = new JwtSecurityTokenHandler()
          .WriteToken(jwtSecurityToken);

            return new JwtToken(token);
            
        }

        private string GetUserEmail(object user) => user switch
        {
            Admin admin => admin.Email,
            Doctor doctor => doctor.Email,
            CustomerService cs => cs.Email,
            _ => throw new InvalidOperationException("Unknown user type")
        };
    }
}
