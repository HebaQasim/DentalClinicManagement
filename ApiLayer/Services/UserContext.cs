using DentalClinicManagement.DomainLayer.Interfaces.IServices;
using System.Security.Claims;

namespace DentalClinicManagement.ApiLayer.Services
{
    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        public Guid Id => Guid.Parse(
            httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ??
            throw new UnauthorizedAccessException("User is not authenticated."));

        public string Role => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role)
                       ?? throw new UnauthorizedAccessException("User is not authenticated.");


        public string Email => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email) ??
                               throw new UnauthorizedAccessException("User is not authenticated.");
    }
}
