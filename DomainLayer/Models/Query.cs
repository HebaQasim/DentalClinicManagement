using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace DentalClinicManagement.DomainLayer.Models
{
    public record Query<TEntity>(
        Expression<Func<TEntity, bool>> Filter,
        SortOrder SortOrder,
        string? SortColumn,
        int PageNumber,
        int PageSize);
}
