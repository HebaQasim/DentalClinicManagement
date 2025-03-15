using AutoMapper;
using DentalClinicManagement.ApiLayer.DTOs.DoctorDTOs;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.InfrastructureLayer.Repositories;
using MediatR;

namespace DentalClinicManagement.ApplicationLayer.DoctorFeatures.GetDoctor.GetAllDoctors
{
    public class GetAllDoctorsCommandHandler : IRequestHandler<GetAllDoctorsCommand, PaginatedList<GetDoctorDto>>
    {
        private readonly IDoctorRepository _repository;
        private readonly IMapper _mapper;

        public GetAllDoctorsCommandHandler(IDoctorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<GetDoctorDto>> Handle(GetAllDoctorsCommand request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllDoctorsAsync(request.PageNumber, request.PageSize);
        }
    }
}
