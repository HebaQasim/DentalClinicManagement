using DentalClinicManagement.DomainLayer.Entities;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.DomainLayer.Models;
using MediatR;
using System.Linq;
using DentalClinicManagement.DomainLayer.Enums;
using System.Linq.Expressions;
using DentalClinicManagement.ApiLayer.DTOs.DoctorDTOs;

namespace DentalClinicManagement.ApplicationLayer.DoctorFeatures.GetDoctor.AddDoctorWithPaginationInTwoMethods
{
    public class GetDoctorsQueryHandler : IRequestHandler<GetDoctorsQuery, PaginatedList<GetDoctorDto>>
    {
        private readonly IPagination<Doctor> _doctorRepository;

        public GetDoctorsQueryHandler(IPagination<Doctor> doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<PaginatedList<Doctor>> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Doctor, bool>> filter = string.IsNullOrEmpty(request.SearchTerm)
     ? (d => true) // في حال عدم تحديد فلتر، يتم جلب جميع السجلات
     : (d => d.FullName.Contains(request.SearchTerm) || d.PhoneNumber.Contains(request.SearchTerm));


            // تحديد القيمة الافتراضية لعدد العناصر في الصفحة إذا لم يتم تحديدها
            var pageSize = request.PageSize > 0 ? request.PageSize : 5;

            // تحديد العمود الافتراضي للترتيب إذا لم يتم تحديده
            var sortColumn = string.IsNullOrEmpty(request.SortColumn) ? "Name" : request.SortColumn;

            return await _doctorRepository.GetPagedAsync(
                filter,
                sortColumn,
                 request.SortOrder.GetValueOrDefault((Microsoft.Data.SqlClient.SortOrder)SortOrder.Ascending), // في حال عدم تحديد الترتيب، يكون تصاعديًا
                request.PageNumber,
                pageSize,
                cancellationToken);
        }

        Task<PaginatedList<GetDoctorDto>> IRequestHandler<GetDoctorsQuery, PaginatedList<GetDoctorDto>>.Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
