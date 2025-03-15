namespace DentalClinicManagement.DomainLayer.Interfaces.IServices
{
    public interface IPaginationParams
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
    }
}
