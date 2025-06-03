using DentalClinicManagement.ApiLayer.DTOs.DoctorDTOs;
using DentalClinicManagement.DomainLayer.Entities;
using DentalClinicManagement.DomainLayer.Models;
using MediatR;
using Microsoft.Data.SqlClient;

namespace DentalClinicManagement.ApplicationLayer.DoctorFeatures.GetDoctor.AddDoctorWithPaginationInTwoMethods
{

    public class GetDoctorsQuery : IRequest<PaginatedList<GetDoctorDto>>
    {
        public string? SearchTerm { get; init; }
        public string? SortColumn { get; init; }
        public SortOrder? SortOrder { get; init; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
}