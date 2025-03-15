using AutoMapper;
using DentalClinicManagement.ApiLayer.DTOs.Auth;
using DentalClinicManagement.ApplicationLayer.Common.ChangePassword;
using DentalClinicManagement.ApplicationLayer.Common.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinicManagement.ApiLayer.Controllers
{
    [Route("api/auth")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly ISender mediator;
        private readonly IMapper mapper;

        public AuthController(ISender mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LoginResponse>> Login(
          LoginRequest loginRequest,
          CancellationToken cancellationToken)
        {
            var loginCommand = mapper.Map<LoginCommand>(loginRequest);

            return Ok(await mediator.Send(loginCommand, cancellationToken));
        }
        [HttpPatch("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            await mediator.Send(command);
            return Ok(new { message = "Password changed successfully" });
        }

    }
}
