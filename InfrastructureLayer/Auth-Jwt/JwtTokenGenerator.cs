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
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtAuthConfig _jwtAuthConfig;

        public JwtTokenGenerator(IOptions<JwtAuthConfig> jwtAuthConfig)
        {
            _jwtAuthConfig = jwtAuthConfig.Value;
        }

        public JwtToken Generate(object user)
        {
            var claims = new List<Claim>();
            string role = string.Empty;
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User object cannot be null.");
            }
            switch (user)
            {
                case Admin admin:
                    if (admin.Role == null) throw new ArgumentException("Admin role is missing.");
                    claims.Add(new Claim("sub", admin.Id.ToString()));
                    claims.Add(new Claim("fullName", admin.FullName));
                    claims.Add(new Claim("email", admin.Email));
                    
                    claims.Add(new Claim(ClaimTypes.Role, admin.Role.Name));
                    role = "Admin";
                    break;

                case Doctor doctor:
                    if (!doctor.IsActive) throw new UnauthorizedAccessException("Doctor account is inactive.");
                    claims.Add(new Claim("sub", doctor.Id.ToString()));
                    claims.Add(new Claim("fullName", doctor.FullName));
                    claims.Add(new Claim("email", doctor.Email));
                   
                    
                    claims.Add(new Claim(ClaimTypes.Role, doctor.Role.Name));
                    role = "Doctor";
                    break;

                case CustomerService customerService:
                    if (!customerService.IsActive) throw new UnauthorizedAccessException("Customer service account is inactive.");
                    claims.Add(new Claim("sub", customerService.Id.ToString()));
                    claims.Add(new Claim("fullName", customerService.FullName));
                    claims.Add(new Claim("email", customerService.Email));
                   
                    claims.Add(new Claim(ClaimTypes.Role, customerService.Role.Name));
                    role = "CustomerService";
                    break;

                default:
                    throw new ArgumentException("Invalid user type.");
            }

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtAuthConfig.Key)),
                SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                _jwtAuthConfig.Issuer,
                _jwtAuthConfig.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtAuthConfig.LifetimeMinutes),
                signingCredentials: signingCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return new JwtToken(token);
        }
    }
}
