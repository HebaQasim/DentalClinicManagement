using DentalClinicManagement.DomainLayer.Entities;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.InfrastructureLayer.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace DentalClinicManagement.InfrastructureLayer.Repositories
{
    public class TreatmentRepository : ITreatmentRepository
    {
        private readonly DentalClinicDbContext _context;

        public TreatmentRepository(DentalClinicDbContext context)
        {
            _context = context;
        }

        public async Task AddTreatmentAsync(Treatment treatment, CancellationToken cancellationToken)
        {
            await _context.Treatments.AddAsync(treatment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<List<Treatment>> GetAllTreatmentsAsync(CancellationToken cancellationToken)
        {
            return await _context.Treatments.ToListAsync(cancellationToken);
        }
        public async Task<Treatment?> GetTreatmentByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Treatments.FindAsync(new object[] { id }, cancellationToken);
        }
        public async Task DeleteTreatmentAsync(Treatment treatment, CancellationToken cancellationToken)
        {
            _context.Treatments.Remove(treatment);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task UpdateTreatmentAsync(Treatment treatment)
        {
            _context.Treatments.Update(treatment);
            await _context.SaveChangesAsync();
        }
        

    }

}
