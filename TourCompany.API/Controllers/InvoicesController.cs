using MediatR;
using Microsoft.AspNetCore.Mvc;
using TourCompany.Application.Invoices.Commands.InsertInvoice;
using TourCompany.Application.Invoices.Queries.GetInvoice;
using TourCompany.Application.Invoices.Queries.GetPaginatedInvoices;
using TourCompany.Application.Invoices.Queries.GetUnpaidBookingsByTourist;

namespace TourCompany.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvoicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] string? filter)
        {
            var tours = await _mediator.Send(new GetPaginatedInvoicesQuery()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Filter = filter
            });
            return Ok(tours);
        }

        [HttpGet("{no}")]
        public async Task<IActionResult> Get([FromRoute] string no)
        {
            var tour = await _mediator.Send(new GetInvoiceQuery()
            {
                InvoiceNo = no,
            });

            return Ok(tour);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUnpaidBookingsByTourist([FromRoute] int id)
        {
            var unpaidBookings = await _mediator.Send(new GetUnpaidBookingsByTouristQuery()
            {
                TouristID = id,
            });

            return Ok(unpaidBookings);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] InsertInvoiceCommand command)
        {
            var tour = await _mediator.Send(command);
            return Ok(tour.InvoiceID);
        }
    }
}
