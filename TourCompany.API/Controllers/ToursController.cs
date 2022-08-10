using MediatR;
using Microsoft.AspNetCore.Mvc;
using TourCompany.Application.Tours.Commands.DeleteTour;
using TourCompany.Application.Tours.Commands.InsertTour;
using TourCompany.Application.Tours.Commands.UpdateTour;
using TourCompany.Application.Tours.Queries.GetPaginatedTours;
using TourCompany.Application.Tours.Queries.GetTour;

namespace TourCompany.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ToursController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ToursController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var tours = await _mediator.Send(new GetPaginatedToursQuery()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            });
            return Ok(tours);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var tour = await _mediator.Send(new GetTourQuery()
            {
                TourID = id,
            });

            return Ok(tour);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] InsertTourCommand command)
        {
            var tour = await _mediator.Send(command);
            return Ok(tour.ID);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] UpdateTourCommand command)
        {
            command.TourID = id;
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _mediator.Send(new DeleteTourCommand() { TourID = id });
            return Ok();
        }
    }
}
