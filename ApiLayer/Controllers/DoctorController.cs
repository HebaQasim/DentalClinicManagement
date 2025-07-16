using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DentalClinicManagement.InfrastructureLayer.Repositories;
using DentalClinicManagement.ApplicationLayer.DoctorFeatures.AddDoctor;
using DentalClinicManagement.ApiLayer.DTOs.DoctorDTOs;
using DentalClinicManagement.ApplicationLayer.DoctorFeatures.DeleteDoctor;
using DentalClinicManagement.ApplicationLayer.DoctorFeatures.UpdateDoctor;
using DentalClinicManagement.ApplicationLayer.DoctorFeatures.GetDoctor;
using DentalClinicManagement.ApplicationLayer.Common.ChangePassword;
using Microsoft.AspNetCore.Authorization;
using DentalClinicManagement.ApiLayer.Extensions;
using DentalClinicManagement.ApplicationLayer.DoctorFeatures.GetDoctor.AddDoctorWithPaginationInTwoMethods;
using DentalClinicManagement.ApplicationLayer.DoctorFeatures.GetDoctor.GetAllDoctorsWithoutPagination;
using DentalClinicManagement.ApplicationLayer.DoctorFeatures.SearchDoctor;
using DentalClinicManagement.ApplicationLayer.DoctorFeatures.DoctorFilter;
using Microsoft.AspNetCore.Components.Forms;

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
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> AddDoctor(AddDoctorRequest request)
        {
            var command = _mapper.Map<AddDoctorCommand>(request);
            var doctorId = await _mediator.Send(command);
            return Ok(new { DoctorId = doctorId });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDoctor(Guid id)
        {
            var command = new DeleteDoctorCommand(id);
            var result = await _mediator.Send(command);

            if (!result)
                return NotFound("Doctor not found.");

            return Ok("Doctor deleted successfully.");
        }

        //[HttpGet]
        //public async Task<ActionResult<PaginatedList<GetDoctorDto>>> GetDoctors(int pageNumber = 1, int pageSize = 10)
        //{
        //    var query = new GetAllDoctorsCommand { PageNumber = pageNumber, PageSize = pageSize };
        //    var doctors = await _mediator.Send(query);

        //    return Ok(doctors);
        //}
        //[HttpGet]
        //public async Task<IActionResult> GetDoctors([FromQuery] DoctorsGetRequest request)
        //{
        //    var query = _mapper.Map<GetDoctorsQuery>(request);
        //    var result = await _mediator.Send(query);

        //    // Add pagination header
        //    Response.Headers.AddPaginationMetadata(result.PaginationMetadata);

        //    return Ok(result.Items);
        //}
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllDoctors(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllDoctorsCommand(), cancellationToken);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetDoctorDto>> GetDoctorById(Guid id)
        {
            var query = new GetDoctorByIdCommand(id);
            var doctor = await _mediator.Send(query);

            return Ok(doctor);
        }
        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateDoctor(Guid id, [FromBody] UpdateDoctorCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);

            if (!result.Success)
                return NotFound(result.WarningMessage);

            if (!string.IsNullOrEmpty(result.WarningMessage))
            {
                return Ok(new
                {
                    message = "Doctor updated successfully",
                    warning = result.WarningMessage
                });
            }

            return Ok("Doctor updated successfully");
        }
        [Authorize(Roles = "Doctor")]
        [HttpPatch("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            var response = await _mediator.Send(command);
            if (!response.Success)
                return BadRequest(response.WarningMessage);

            return Ok(new { Message = response.WarningMessage, RequireReLogin = response.RequireReLogin });
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("search")]
        public async Task<IActionResult> SearchDoctors([FromQuery] string? name, [FromQuery] string? phoneNumber)
        {
            var query = new GetDoctorByNameOrPhoneCommand
            {
                Name = name,
                PhoneNumber = phoneNumber
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }
         [Authorize(Roles = "Admin")]
        [HttpGet("filter")]
        public async Task<IActionResult> FilterBySpecialization([FromQuery] string specialization)
        {
            var query = new GetDoctorsBySpecializationCommand { Specialization = specialization };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
