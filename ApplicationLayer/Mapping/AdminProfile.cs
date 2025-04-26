using AutoMapper;
using DentalClinicManagement.ApiLayer.DTOs.AdminDTOs;
using DentalClinicManagement.ApplicationLayer.AdminFeatures.UpdateAdmin;
using DentalClinicManagement.ApplicationLayer.Common.ChangePassword;
using DentalClinicManagement.DomainLayer.Entities;

namespace DentalClinicManagement.ApplicationLayer.Mapping
{
    public class AdminProfile : Profile
    {
        public AdminProfile() {
            CreateMap<Admin, GetAdminDto>();
            CreateMap<UpdateAdminCommand, Admin>().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ChangePasswordCommand, Admin>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());
        }
    }
}
