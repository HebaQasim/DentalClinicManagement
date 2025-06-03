namespace DentalClinicManagement.DomainLayer.Models
{
    public record PaginatedList<T>(
   IEnumerable<T> Items,
   PaginationMetadata PaginationMetadata);
}
