using DentalClinicManagement.DomainLayer.Models;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace DentalClinicManagement.DomainLayer.Interfaces.IRepository
{
    public interface IPagination<T>
    {
        Task<PaginatedList<T>> GetPagedAsync(
            Expression<Func<T, bool>> filter,
            string? sortColumn,
            SortOrder sortOrder,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken);
    }
}
