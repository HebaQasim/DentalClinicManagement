using AutoMapper;
using DentalClinicManagement.ApiLayer.DTOs.DoctorDTOs;
using DentalClinicManagement.DomainLayer.Entities;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.DomainLayer.Models;
using DentalClinicManagement.InfrastructureLayer.DbContexts;
using DentalClinicManagement.InfrastructureLayer.Extensions;
using DentalClinicManagement.InfrastructureLayer.Helper;
using Microsoft.EntityFrameworkCore;

namespace DentalClinicManagement.InfrastructureLayer.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DentalClinicDbContext _context;
        private readonly IMapper _mapper;

        public DoctorRepository(DentalClinicDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> GetDoctorRoleId()
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Doctor");
            return role == null ? throw new InvalidOperationException("Doctor role not found.") : role.Id;
        }
        public async Task AddDoctorAsync(Doctor doctor, CancellationToken cancellationToken)
        {
            await _context.Doctors.AddAsync(doctor, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<List<Doctor>> GetAllDoctorsAsync(CancellationToken cancellationToken)
        {
            return await _context.Doctors.ToListAsync(cancellationToken);
        }
        public async Task<bool> ExistsByEmailAsync(string email,
     CancellationToken cancellationToken = default)
        {
            return await _context.Doctors.AnyAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<bool> ExistsByIdAsync(Guid id,
          CancellationToken cancellationToken = default)
        {
            return await _context.Doctors
              .AnyAsync(u => u.Id == id, cancellationToken);
        }

    //    public async Task<PaginatedList<GetDoctorDto>> GetAllDoctorsAsync(int pageNumber, int pageSize)
    //    {
    //        var query = _context.Doctors.AsNoTracking();
    //        return await PaginatedList<GetDoctorDto>.CreateAsync(
    //    query.Select(d => _mapper.Map<GetDoctorDto>(d)),
    //    pageNumber,
    //    pageSize
    //);
    //    }
        public async Task<Doctor?> GetDoctorByIdAsync(Guid id)
        {
            return await _context.Doctors
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<string>> GetAllDoctorColorsAsync()
        {
            return await _context.Doctors.Select(d => d.ColorCode).ToListAsync();
        }
        //public async Task<PaginatedList<Doctor>> GetDoctorAsync(Query<Doctor> query, CancellationToken cancellationToken)
        //{
        //    var queryable = _context.Doctors
        //        .Where(query.Filter)
        //        .Sort(SortingExpressions.GetDoctorSortExpression(query.SortColumn), (DomainLayer.Enums.SortOrder)query.SortOrder);

        //    var itemsToReturn = await queryable
        //        .GetPage(query.PageNumber, query.PageSize)
        //        .AsNoTracking()
        //        .ToListAsync(cancellationToken);

        //    var metadata = await queryable.GetPaginationMetadataAsync(query.PageNumber, query.PageSize);

        //    return new PaginatedList<Doctor>(itemsToReturn, metadata);
        //}
        public async Task<Doctor?> GetByEmailAsync(string email)
        {
            return await _context.Doctors
                .Include(d => d.Role)
                .FirstOrDefaultAsync(d => d.Email.ToLower() == email.ToLower());
        }

    }
}
