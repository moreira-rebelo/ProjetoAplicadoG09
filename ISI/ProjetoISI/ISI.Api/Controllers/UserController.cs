using ISI.Api.ApiModels;
using ISI.Api.Extensions;
using ISI.Application.UseCases.User.GetUser;
using ISI.Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ISI.Api.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private readonly IMediator _mediator;
        private readonly IJwtToken _jwtToken;
        
        public UserController(IMediator mediator, IJwtToken jwtToken)
        {
            _mediator = mediator;
            _jwtToken = jwtToken;

        }
        
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<GetUserOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserAsync()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token not provided");
            }

            var reservationFromToken = _jwtToken.GetReservationFromToken(token);

            if (reservationFromToken == null)
            {
                return Unauthorized("Invalid token");
            }

            var output = await _mediator.Send(new GetUserInput(reservationFromToken), CancellationToken.None);

            return Ok(new ApiResponse<GetUserOutput>(output));        }
    }
}
