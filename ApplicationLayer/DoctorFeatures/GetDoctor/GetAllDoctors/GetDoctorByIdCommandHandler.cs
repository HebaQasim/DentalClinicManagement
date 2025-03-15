using AutoMapper;
using DentalClinicManagement.ApiLayer.DTOs.DoctorDTOs;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using MediatR;

namespace DentalClinicManagement.ApplicationLayer.DoctorFeatures.GetDoctor.GetAllDoctors
{
    public class GetDoctorByIdCommandHandler : IRequestHandler<GetDoctorByIdCommand, GetDoctorDto>
    {
        private readonly IDoctorRepository _repository;
        private readonly IMapper _mapper;

        public GetDoctorByIdCommandHandler(IDoctorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetDoctorDto> Handle(GetDoctorByIdCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _repository.GetDoctorByIdAsync(request.Id);

            if (doctor == null)
            {
                throw new KeyNotFoundException("Doctor not found.");
            }

            return _mapper.Map<GetDoctorDto>(doctor);
        }
    }
}
