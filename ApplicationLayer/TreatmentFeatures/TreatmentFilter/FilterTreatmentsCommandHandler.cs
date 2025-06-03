using AutoMapper;
using DentalClinicManagement.ApiLayer.DTOs.TreatmentDTOs;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using MediatR;

namespace DentalClinicManagement.ApplicationLayer.TreatmentFeatures.TreatmentFilter
{

    public class FilterTreatmentsCommandHandler : IRequestHandler<FilterTreatmentsCommand, List<GetTreatmentDto>>
    {
        private readonly ITreatmentRepository _repository;
        private readonly IMapper _mapper;
        public FilterTreatmentsCommandHandler(ITreatmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetTreatmentDto>> Handle(FilterTreatmentsCommand request, CancellationToken cancellationToken)
        {
            var treatments = await _repository.GetAllTreatmentsAsync(cancellationToken);

            // Normalize and filter by category if provided
            if (!string.IsNullOrWhiteSpace(request.Category))
            {
                var normalizedInputCategory = new string(request.Category
                    .Where(c => !char.IsWhiteSpace(c))
                    .ToArray())
                    .ToLower();

                treatments = treatments
                    .Where(t =>
                        new string(t.Category
                            .Where(c => !char.IsWhiteSpace(c))
                            .ToArray())
                            .ToLower() == normalizedInputCategory)
                    .ToList();
            }

            // Filter by exact price if provided
            if (request.Price.HasValue)
            {
                treatments = treatments
                    .Where(t => t.Price == request.Price.Value)
                    .ToList();
            }

            return _mapper.Map<List<GetTreatmentDto>>(treatments);

            
        }
    }
}
