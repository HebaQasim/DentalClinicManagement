using DentalClinicManagement.DomainLayer.Models;
using System.Text.Json;

namespace DentalClinicManagement.ApiLayer.Extensions
{
    public static class ResponseHeadersExtensions
    {
        public static void AddPaginationMetadata(this IHeaderDictionary headers,
          PaginationMetadata paginationMetadata)
        {
            headers["x-pagination"] = JsonSerializer.Serialize(paginationMetadata);
        }
    }
}
