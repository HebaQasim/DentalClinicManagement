using DentalClinicManagement.CustomerServiceFeatures.DeleteCustomerService.DentalClinicManagement.CustomerServiceFeatures.DeleteCustomerService;
using DentalClinicManagement.InfrastructureLayer.DbContexts;
using MediatR;

namespace DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.DeleteCustomerService
{
    public class DeleteCustomerServiceCommandHandler : IRequestHandler<DeleteCustomerServiceCommand, bool>
    {
        private readonly DentalClinicDbContext _context;

        public DeleteCustomerServiceCommandHandler(DentalClinicDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCustomerServiceCommand request, CancellationToken cancellationToken)
        {
            var customerService = await _context.CustomerServices.FindAsync(request.Id);

            if (customerService == null)
                return false;

            _context.CustomerServices.Remove(customerService);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
