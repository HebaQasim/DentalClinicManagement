using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;
using DentalClinicManagement.ApplicationLayer.Common.Login;

namespace DentalClinicManagement.ApplicationLayer.Mapping
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<ApiLayer.DTOs.Auth.LoginRequest, LoginCommand>();

        }
    }
}
