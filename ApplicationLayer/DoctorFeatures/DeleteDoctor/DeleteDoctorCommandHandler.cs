using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.DomainLayer.Interfaces.IServices;
using DentalClinicManagement.DomainLayer.Models;
using DentalClinicManagement.InfrastructureLayer.DbContexts;
using MediatR;
using System.Security.Authentication;

namespace DentalClinicManagement.ApplicationLayer.DoctorFeatures.DeleteDoctor
{
    public class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand, bool>
    {
        private readonly DentalClinicDbContext _context;
        private readonly IAdminRepository _adminRepository;
        private readonly IUserContext _userContext;

        public DeleteDoctorCommandHandler(DentalClinicDbContext context, IAdminRepository adminRepository, IUserContext userContext)
        {
            _context = context;
            _adminRepository = adminRepository;
            _userContext = userContext;
        }

        public async Task<bool> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
        {
            if (!await _adminRepository.ExistsByIdAsync(_userContext.Id, cancellationToken))
            {
                throw new KeyNotFoundException($"Admin not found.");
            }
            if (_userContext.Role != UserRoles.Admin)
            {
                throw new AuthenticationException("Access denied. Only an admin can update admin data.");
            }
            var doctor = await _context.Doctors.FindAsync(request.Id);

            if (doctor == null)
                return false;

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
