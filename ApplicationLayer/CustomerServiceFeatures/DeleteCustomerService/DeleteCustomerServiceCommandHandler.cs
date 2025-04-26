using DentalClinicManagement.CustomerServiceFeatures.DeleteCustomerService.DentalClinicManagement.CustomerServiceFeatures.DeleteCustomerService;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.DomainLayer.Interfaces.IServices;
using DentalClinicManagement.DomainLayer.Models;
using DentalClinicManagement.InfrastructureLayer.DbContexts;
using MediatR;
using System.Security.Authentication;

namespace DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.DeleteCustomerService
{
    public class DeleteCustomerServiceCommandHandler : IRequestHandler<DeleteCustomerServiceCommand, bool>
    {
        private readonly DentalClinicDbContext _context;
        private readonly IAdminRepository _adminRepository;
        private readonly IUserContext _userContext;

        public DeleteCustomerServiceCommandHandler(DentalClinicDbContext context,IAdminRepository adminRepository,IUserContext userContext)
        {
            _context = context;
            _adminRepository = adminRepository;
            _userContext = userContext;
        }

        public async Task<bool> Handle(DeleteCustomerServiceCommand request, CancellationToken cancellationToken)
        {
            if (!await _adminRepository.ExistsByIdAsync(_userContext.Id, cancellationToken))
            {
                throw new KeyNotFoundException($"Admin not found.");
            }
            if (_userContext.Role != UserRoles.Admin)
            {
                throw new AuthenticationException("Access denied. Only an admin can update admin data.");
            }
            var customerService = await _context.CustomerServices.FindAsync(request.Id);

            if (customerService == null)
                return false;

            _context.CustomerServices.Remove(customerService);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
