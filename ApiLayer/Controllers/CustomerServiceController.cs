using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MediatR;
using DentalClinicManagement.CustomerServiceFeatures.DeleteCustomerService.DentalClinicManagement.CustomerServiceFeatures.DeleteCustomerService;
using DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.AddCustomerService;
using DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.UpdateCustomerService;
using DentalClinicManagement.ApiLayer.DTOs.CustomerServiceDTOs;
using DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.GetCustomerService;

namespace DentalClinicManagement.ApiLayer.Controllers
{
    [Route("api/customerService")]
    [ApiController]
    public class CustomerServiceController : Controller
    {
        private readonly ISender mediator;
        private readonly IMapper mapper;

        public CustomerServiceController(ISender mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }
        [HttpPost]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCustomerService(AddCustomerServiceRequest request)
        {
            var command = mapper.Map<AddCustomerServiceCommand>(request);
            var customerServiceId = await mediator.Send(command);
            return Ok(new { CustomerServiceId = customerServiceId });
        }
        [HttpDelete("{id}")]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCustomerService(Guid id)
        {
            var command = new DeleteCustomerServiceCommand(id);
            var result = await mediator.Send(command);

            if (!result)
                return NotFound("Customer Service user not found.");

            return Ok("Customer Service user deleted successfully.");
        }

        [HttpGet]
        // [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<GetCustomerServiceDto>>> GetAllCustomerService()
        {
            var result = await mediator.Send(new GetAllCustomerServicesCommand());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCustomerServiceDto>> GetSecretaryById(Guid id)
        {
            var query = new GetCustomerServiceByIdCommand(id);
            var secretary = await mediator.Send(query);

            return Ok(secretary);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCustomerService(Guid id, [FromBody] UpdateCustomerServiceCommand command)
        {
            command.Id = id;

            var result = await mediator.Send(command);
            return result ? Ok("Customer Service updated successfully") : NotFound("Customer Service not found");
        }

    }
}
