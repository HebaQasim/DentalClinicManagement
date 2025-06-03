using DentalClinicManagement.DomainLayer.Entities;

namespace DentalClinicManagement.DomainLayer.Interfaces.IRepository
{
    public interface ITreatmentRepository
    {
        Task AddTreatmentAsync(Treatment treatment, CancellationToken cancellationToken);
        Task<List<Treatment>> GetAllTreatmentsAsync(CancellationToken cancellationToken);
        Task<Treatment?> GetTreatmentByIdAsync(Guid id, CancellationToken cancellationToken);
        Task DeleteTreatmentAsync(Treatment treatment, CancellationToken cancellationToken);
        Task UpdateTreatmentAsync(Treatment treatment);

    }
}
