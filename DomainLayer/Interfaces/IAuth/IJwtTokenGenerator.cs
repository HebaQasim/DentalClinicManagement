using DentalClinicManagement.DomainLayer.Models;

namespace DentalClinicManagement.DomainLayer.Interfaces.IAuth
{
    public interface IJwtTokenGenerator
    {
        JwtToken Generate(object user);
    }
}
