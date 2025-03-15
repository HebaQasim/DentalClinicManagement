using DentalClinicManagement.InfrastructureLayer.DbContexts;
using MediatR;

namespace DentalClinicManagement.ApplicationLayer.DoctorFeatures.DeleteDoctor
{
    public class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand, bool>
    {
        private readonly DentalClinicDbContext _context;

        public DeleteDoctorCommandHandler(DentalClinicDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _context.Doctors.FindAsync(request.Id);

            if (doctor == null)
                return false;

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
