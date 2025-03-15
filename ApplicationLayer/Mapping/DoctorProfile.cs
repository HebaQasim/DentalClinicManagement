using AutoMapper;
using DentalClinicManagement.ApiLayer.DTOs.DoctorDTOs;
using DentalClinicManagement.ApplicationLayer.DoctorFeatures.AddDoctor;
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
        }

    }
}
