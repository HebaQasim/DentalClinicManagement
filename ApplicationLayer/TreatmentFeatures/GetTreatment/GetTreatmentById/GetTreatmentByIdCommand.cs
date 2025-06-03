using DentalClinicManagement.ApiLayer.DTOs.TreatmentDTOs;
using MediatR;

namespace DentalClinicManagement.ApplicationLayer.TreatmentFeatures.GetTreatment.GetTreatmentById
{
    
        public class GetTreatmentByIdCommand : IRequest<GetTreatmentDto>
        {
            public Guid Id { get; set; }

            public GetTreatmentByIdCommand(Guid id)
            {
                Id = id;
            }
        }
    }

