using MediatR;

namespace DentalClinicManagement.ApplicationLayer.DoctorFeatures.AddDoctor
{
    public class AddDoctorCommand : IRequest<Guid>
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string PhoneNumber { get; init; }
        public string Specialization { get; init; }
    }
}
