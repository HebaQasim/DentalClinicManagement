using AutoMapper;
using DentalClinicManagement.ApiLayer.DTOs.DoctorDTOs;
using DentalClinicManagement.ApplicationLayer.Common.ChangePassword;
using DentalClinicManagement.ApplicationLayer.DoctorFeatures.AddDoctor;
using DentalClinicManagement.ApplicationLayer.DoctorFeatures.GetDoctor.AddDoctorWithPaginationInTwoMethods;
using DentalClinicManagement.ApplicationLayer.DoctorFeatures.UpdateDoctor;
using DentalClinicManagement.DomainLayer.Entities;

namespace DentalClinicManagement.ApplicationLayer.Mapping
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<AddDoctorRequest, AddDoctorCommand>();
            CreateMap<AddDoctorCommand, Doctor>();
            CreateMap<Doctor, GetDoctorDto>();
            CreateMap<UpdateDoctorCommand, Doctor>()
    .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ChangePasswordCommand, Doctor>()
           .ForMember(dest => dest.Password, opt => opt.Ignore());
            CreateMap<DoctorsGetRequest, GetDoctorsQuery>();

        }

    }
}
