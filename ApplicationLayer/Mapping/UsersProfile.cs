using AutoMapper;
using DentalClinicManagement.ApplicationLayer.Common.Login;
using DentalClinicManagement.DomainLayer.Models;

namespace DentalClinicManagement.ApplicationLayer.Mapping
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<JwtToken, LoginResponse>();

        }
    }
}
