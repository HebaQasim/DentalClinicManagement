using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.DomainLayer.Models;
using DentalClinicManagement.InfrastructureLayer.DbContexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DentalClinicManagement.InfrastructureLayer.Repositories
{
    public class PaginationRepository<T> : IPagination<T> where T : class
    {
        private readonly DentalClinicDbContext _context;

        public PaginationRepository(DentalClinicDbContext context) => _context = context;

        public async Task<PaginatedList<T>> GetPagedAsync(
            Expression<Func<T, bool>> filter,
            string? sortColumn,
            SortOrder sortOrder,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken)
        {
            IQueryable<T> query = _context.Set<T>().Where(filter);

            // تطبيق الترتيب
            if (!string.IsNullOrEmpty(sortColumn))
            {
                query = sortOrder == SortOrder.Ascending
                    ? query.OrderBy(e => EF.Property<object>(e, sortColumn))
                    : query.OrderByDescending(e => EF.Property<object>(e, sortColumn));
            }

            // حساب العدد الكلي
            var totalCount = await query.CountAsync(cancellationToken);

            // تطبيق الـ Pagination
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return  new PaginatedList<T>(items, new PaginationMetadata(totalCount, pageNumber, pageSize));
        }

       
    }
}
