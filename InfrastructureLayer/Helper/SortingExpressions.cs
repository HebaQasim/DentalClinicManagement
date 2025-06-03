using DentalClinicManagement.DomainLayer.Entities;
using System.Linq.Expressions;

namespace DentalClinicManagement.InfrastructureLayer.Helper
{
    public static class SortingExpressions
    {
        public static Expression<Func<Doctor, object>> GetDoctorSortExpression(string? sortColumn)
        {
            return sortColumn?.ToLower() switch
            {
                "fullname" => d => d.FullName,
                "email" => d => d.Email,
                "phonenumber" => d => d.PhoneNumber,
                _ => d => d.FullName // default
            };
        }
    }
}
