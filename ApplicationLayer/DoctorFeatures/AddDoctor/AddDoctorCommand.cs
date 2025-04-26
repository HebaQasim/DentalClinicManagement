using MediatR;

namespace DentalClinicManagement.ApplicationLayer.DoctorFeatures.AddDoctor
{
    public class AddDoctorCommand : IRequest<Guid>
    {
        public string FullName { get; init; }
        public string WorkingTime { get; init; }
        public string Email { get; init; }
        public string PhoneNumber { get; init; }
        public string Specialization { get; init; }
        public string ColorCode { get; init; }
    }
}
