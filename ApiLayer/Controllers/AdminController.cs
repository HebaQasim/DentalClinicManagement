using AutoMapper;
using DentalClinicManagement.ApiLayer.DTOs.AdminDTOs;
using DentalClinicManagement.ApplicationLayer.AdminFeatures.GetAdmin;
using DentalClinicManagement.ApplicationLayer.AdminFeatures.UpdateAdmin;
using DentalClinicManagement.ApplicationLayer.Common.ChangePassword;
using DentalClinicManagement.ApplicationLayer.CustomerServiceFeatures.UpdateCustomerService;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinicManagement.ApiLayer.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly ISender mediator;
        private readonly IMapper mapper;
        public AdminController(ISender mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAdmin(Guid id, [FromBody] UpdateAdminCommand command)
        {
            command.Id = id;
            var result = await mediator.Send(command);

            if (!result.Success)
                return NotFound("Customer Service not found");

            if (!string.IsNullOrEmpty(result.WarningMessage))
            {
                return Ok(new
                {
                    message = "Admin updated successfully",
                    warning = result.WarningMessage
                });
            }

            return Ok("Admin updated successfully");
        }
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
        {
            var profile = await mediator.Send(new GetAdminByIdCommand(), cancellationToken);
            return Ok(profile);
        }
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
