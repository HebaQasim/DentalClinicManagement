using MediatR;

namespace DentalClinicManagement.ApplicationLayer.DoctorFeatures.DeleteDoctor
{
    public class DeleteDoctorCommand : IRequest<bool>
    {

        public Guid Id { get; }

        public DeleteDoctorCommand(Guid id)
        {
            Id = id;
        }

    }
}
