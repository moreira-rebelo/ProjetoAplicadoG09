using ISI.Api.ApiModels;
using ISI.Api.Extensions;
using ISI.Application.UseCases.Reservation.CreateEntry;
using ISI.Application.UseCases.Reservation.CreateReservation;
using ISI.Application.UseCases.Reservation.GetReservation;
using ISI.Application.UseCases.Reservation.LoginReservation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISI.Api.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IJwtToken _jwtToken;

        public ReservationController(IMediator mediator, IJwtToken jwtToken)
        {
            _mediator = mediator;
            _jwtToken = jwtToken;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<GetReservationOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReservationAsync()
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

            var output = await _mediator.Send(new GetReservationInput(reservationFromToken), CancellationToken.None);

            return Ok(new ApiResponse<GetReservationOutput>(output));
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponse<LoginReservationOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> ReservationLoginAsync([FromBody] LoginReservationInput input,
            CancellationToken cancellationToken)
        {
            var output = await _mediator.Send(input, cancellationToken);
            return Ok(new ApiResponse<LoginReservationOutput>(output));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<CreateReservationOutput>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationInput input,
            CancellationToken cancellationToken)
        {
            var output = await _mediator.Send(input, cancellationToken);
            var returnValue =
                CreatedAtAction(nameof(CreateReservation), new ApiResponse<CreateReservationOutput>(output));
            
            return returnValue;
        }


        [Authorize]
        [HttpPost("entries")]
        [ProducesResponseType(typeof(ApiResponse<CreateEntryOutput>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateEntry([FromBody] CreateEntryInput input,
            CancellationToken cancellationToken)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            Console.WriteLine(token);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token not provided");
            }

            var reservationFromToken = _jwtToken.GetReservationFromToken(token);

            if (reservationFromToken == null)
            {
                return Unauthorized("Invalid token");
            }
            
            var output = await _mediator.Send(input, cancellationToken);
            return Ok(new ApiResponse<CreateEntryOutput>(output));
        }
    }
}