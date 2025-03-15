using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DentalClinicManagement.InfrastructureLayer.Repositories;
using DentalClinicManagement.ApplicationLayer.DoctorFeatures.AddDoctor;
using DentalClinicManagement.ApiLayer.DTOs.DoctorDTOs;
using DentalClinicManagement.ApplicationLayer.DoctorFeatures.DeleteDoctor;
using DentalClinicManagement.ApplicationLayer.DoctorFeatures.GetDoctor.GetAllDoctors;

namespace DentalClinicManagement.ApiLayer.Controllers
{
    [Route("api/doctor")]
    [ApiController]
    public class DoctorController : Controller
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public DoctorController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]

        public async Task<IActionResult> AddDoctor(AddDoctorRequest request)
        {
            var command = _mapper.Map<AddDoctorCommand>(request);
            var doctorId = await _mediator.Send(command);
            return Ok(new { DoctorId = doctorId });
        }

        [HttpDelete("{id}")]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDoctor(Guid id)
        {
            var command = new DeleteDoctorCommand(id);
            var result = await _mediator.Send(command);

            if (!result)
                return NotFound("Doctor not found.");

            return Ok("Doctor deleted successfully.");
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<GetDoctorDto>>> GetDoctors(int pageNumber = 1, int pageSize = 10)
        {
            var query = new GetAllDoctorsCommand { PageNumber = pageNumber, PageSize = pageSize };
            var doctors = await _mediator.Send(query);

            return Ok(doctors);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GetDoctorDto>> GetDoctorById(Guid id)
        {
            var query = new GetDoctorByIdCommand(id);
            var doctor = await _mediator.Send(query);

            return Ok(doctor);
        }
    }
}
