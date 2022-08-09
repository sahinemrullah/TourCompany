using MediatR;
using Microsoft.AspNetCore.Mvc;
using TourCompany.Application.Bookings.Commands.InsertBooking;
using TourCompany.Application.Bookings.Commands.UpdateBooking;
using TourCompany.Application.Bookings.Queries.GetBooking;
using TourCompany.Application.Bookings.Queries.GetPaginatedBookings;

namespace TourCompany.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var bookings = await _mediator.Send(new GetPaginatedBookingsQuery()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            });
            return Ok(bookings);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var booking = await _mediator.Send(new GetBookingQuery()
            {
                BookingID = id,
            });

            return Ok(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] InsertBookingCommand command)
        {
            var booking = await _mediator.Send(command);
            return Ok(booking.BookingID);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] UpdateBookingCommand command)
        {
            command.BookingID = id;
            await _mediator.Send(command);
            return Ok();
        }
    }
}
