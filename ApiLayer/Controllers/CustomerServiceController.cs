using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MediatR;
using DentalClinicManagement.CustomerServiceFeatures.DeleteCustomerService.DentalClinicManagement.CustomerServiceFeatures.DeleteCustomerService;
using DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.AddCustomerService;
using DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.UpdateCustomerService;
using DentalClinicManagement.ApiLayer.DTOs.CustomerServiceDTOs;
using DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.GetCustomerService;
using DentalClinicManagement.ApplicationLayer.Common.ChangePassword;
using Microsoft.AspNetCore.Authorization;

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
         [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCustomerService(AddCustomerServiceRequest request)
        {
            var command = mapper.Map<AddCustomerServiceCommand>(request);
            var customerServiceId = await mediator.Send(command);
            return Ok(new { CustomerServiceId = customerServiceId });
        }
        [HttpDelete("{id}")]
         [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCustomerService(Guid id)
        {
            var command = new DeleteCustomerServiceCommand(id);
            var result = await mediator.Send(command);

            if (!result)
                return NotFound("Customer Service user not found.");

            return Ok("Customer Service user deleted successfully.");
        }

        [HttpGet]
         [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<GetCustomerServiceDto>>> GetAllCustomerService()
        {
            var result = await mediator.Send(new GetAllCustomerServicesCommand());
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCustomerServiceDto>> GetSecretaryById(Guid id)
        {
            var query = new GetCustomerServiceByIdCommand(id);
            var secretary = await mediator.Send(query);

            return Ok(secretary);
        }
         [Authorize(Roles = "Admin")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCustomerService(Guid id, [FromBody] UpdateCustomerServiceCommand command)
        {
            command.Id = id;
            var result = await mediator.Send(command);

            if (!result.Success)
                return NotFound("Customer Service not found");

            if (!string.IsNullOrEmpty(result.WarningMessage))
            {
                return Ok(new
                {
                    message = "Customer Service updated successfully",
                    warning = result.WarningMessage
                });
            }

            return Ok("Customer Service updated successfully");
        }
        [Authorize(Roles = "CustomerService")]
        [HttpPatch("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            var response = await mediator.Send(command);
            if (!response.Success)
                return BadRequest(response.WarningMessage);

            return Ok(new { Message = response.WarningMessage, RequireReLogin = response.RequireReLogin });
        }
    }
}
