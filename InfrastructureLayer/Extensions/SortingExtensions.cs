using DentalClinicManagement.DomainLayer.Enums;
using System.ComponentModel;
using System.Linq.Expressions;

namespace DentalClinicManagement.InfrastructureLayer.Extensions
{
    public static class SortingExtensions
    {
        public static IQueryable<TItem> Sort<TItem>(
          this IQueryable<TItem> queryable,
          Expression<Func<TItem, object>> sortColumnExpression,
          SortOrder sortOrder)
        {
            return sortOrder switch
            {
                SortOrder.Ascending => queryable.OrderBy(sortColumnExpression),
                SortOrder.Descending => queryable.OrderByDescending(sortColumnExpression),
                _ => throw new InvalidEnumArgumentException()
            };
        }
    }
}
