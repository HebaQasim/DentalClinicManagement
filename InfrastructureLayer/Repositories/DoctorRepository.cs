using AutoMapper;
using DentalClinicManagement.ApiLayer.DTOs.DoctorDTOs;
using DentalClinicManagement.DomainLayer.Entities;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.InfrastructureLayer.DbContexts;
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
        public async Task<PaginatedList<GetDoctorDto>> GetAllDoctorsAsync(int pageNumber, int pageSize)
        {
            var query = _context.Doctors.AsNoTracking();
            return await PaginatedList<GetDoctorDto>.CreateAsync(
        query.Select(d => _mapper.Map<GetDoctorDto>(d)),
        pageNumber,
        pageSize
    );
        }
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
    }
}
