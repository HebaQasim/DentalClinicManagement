using AutoMapper;
using DentalClinicManagement.ApiLayer.DTOs.TreatmentDTOs;
using DentalClinicManagement.ApplicationLayer.TreatmentFeatures.AddTreatment;
using DentalClinicManagement.ApplicationLayer.TreatmentFeatures.UpdateTreatment;
using DentalClinicManagement.DomainLayer.Entities;

namespace DentalClinicManagement.ApplicationLayer.Mapping
{
    public class TreatmentProfile : Profile
    {
        public TreatmentProfile()
        {
            CreateMap<AddTreatmentRequest, AddTreatmentCommand>();
            CreateMap<AddTreatmentCommand, Treatment>();
            CreateMap<Treatment, GetTreatmentDto>();
            CreateMap<UpdateTreatmentRequest, UpdateTreatmentCommand>();
            CreateMap<UpdateTreatmentCommand, Treatment>()
            .ForAllMembers(opt => opt.Condition((src, dest, member) => member != null));

        }
    }
}
