using AutoMapper;
using DentalClinicManagement.ApiLayer.DTOs.CustomerServiceDTOs;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using MediatR;

namespace DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.GetCustomerService
{
    public class GetCustomerServiceByIdCommandHandler : IRequestHandler<GetCustomerServiceByIdCommand, GetCustomerServiceDto>
    {
        private readonly ICustomerServiceRepository _repository;
        private readonly IMapper _mapper;

        public GetCustomerServiceByIdCommandHandler(ICustomerServiceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetCustomerServiceDto> Handle(GetCustomerServiceByIdCommand request, CancellationToken cancellationToken)
        {
            var secretary = await _repository.GetCustomerServiceByIdAsync(request.Id);

            if (secretary == null)
            {
                throw new KeyNotFoundException("Secretary not found.");
            }

            return _mapper.Map<GetCustomerServiceDto>(secretary);
        }
    }
}
