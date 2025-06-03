using AutoMapper;
using DentalClinicManagement.ApiLayer.DTOs.TreatmentDTOs;
using DentalClinicManagement.ApplicationLayer.DoctorFeatures.DoctorFilter;
using DentalClinicManagement.ApplicationLayer.TreatmentFeatures.AddTreatment;
using DentalClinicManagement.ApplicationLayer.TreatmentFeatures.DeleteTreatment;
using DentalClinicManagement.ApplicationLayer.TreatmentFeatures.GetTreatment.GetAllTreatments;
using DentalClinicManagement.ApplicationLayer.TreatmentFeatures.GetTreatment.GetTreatmentById;
using DentalClinicManagement.ApplicationLayer.TreatmentFeatures.SearchTreatment;
using DentalClinicManagement.ApplicationLayer.TreatmentFeatures.TreatmentFilter;
using DentalClinicManagement.ApplicationLayer.TreatmentFeatures.UpdateTreatment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinicManagement.ApiLayer.Controllers
{
    [Route("api/treatment")]
    [ApiController]
    public class TreatmentController : Controller
    {
        private readonly ISender mediator;
        private readonly IMapper mapper;
        public TreatmentController(ISender mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]

        public async Task<IActionResult> AddTreatment([FromBody] AddTreatmentRequest request)
        {
            var command = mapper.Map<AddTreatmentCommand>(request);
            var id = await mediator.Send(command);
            return Ok(new { TreatmentId = id });
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<GetTreatmentDto>>> GetAllTreatments(CancellationToken cancellationToken)
        {
            var query = new GetAllTreatmentsQuery();
            var result = await mediator.Send(query, cancellationToken);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetTreatmentDto>> GetTreatmentById(Guid id, CancellationToken cancellationToken)
        {
            var command = new GetTreatmentByIdCommand(id);
            var result = await mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTreatment(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteTreatmentCommand(id);
            var result = await mediator.Send(command, cancellationToken);
            return result ? Ok("Treatment deleted successfully.") : NotFound();
        }
        [HttpPatch("{id}")]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTreatment(Guid id, [FromBody] UpdateTreatmentRequest request)
        {
            var command = new UpdateTreatmentCommand
            {
                Id = id,
                Name = request.Name,
                Category = request.Category,
                Price = request.Price
            };
            var result = await mediator.Send(command);
            if (!result)
                return NotFound("Treatment not found.");

            return Ok("Treatment updated successfully.");


        }
        [Authorize(Roles = "Admin")]
        [HttpGet("search")]
        public async Task<IActionResult> SearchTreatment([FromQuery] string name)
        {
            var query = new SearchTreatmentCommand { Name = name };
            var result = await mediator.Send(query);
            return Ok(result);
        }
         [Authorize(Roles = "Admin")]
        [HttpGet("filter")]
        public async Task<IActionResult> FilterTreatments([FromQuery] string? category, [FromQuery] decimal? price)
        {
            var query = new FilterTreatmentsCommand
            {
                Category = category,
                Price = price
            };
            var result = await mediator.Send(query);
            return Ok(result);


        }
    }
}

