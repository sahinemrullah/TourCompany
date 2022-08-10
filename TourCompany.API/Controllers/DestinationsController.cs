using MediatR;
using Microsoft.AspNetCore.Mvc;
using TourCompany.Application.Destinations.Commands.DeleteDestination;
using TourCompany.Application.Destinations.Commands.InsertDestination;
using TourCompany.Application.Destinations.Commands.UpdateDestination;
using TourCompany.Application.Destinations.Queries.GetDestination;
using TourCompany.Application.Destinations.Queries.GetPaginatedDestinations;

namespace TourCompany.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DestinationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DestinationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var destinations = await _mediator.Send(new GetPaginatedDestinationsQuery()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            });
            return Ok(destinations);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var destination = await _mediator.Send(new GetDestinationQuery()
            {
                DestinationID = id,
            });

            return Ok(destination);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _mediator.Send(new DeleteDestinationCommand()
            {
                DestinationID = id
            });

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] InsertDestinationCommand command)
        {
            var destination = await _mediator.Send(command);
            return Ok(destination.ID);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] UpdateDestinationCommand command)
        {
            command.DestinationID = id;
            await _mediator.Send(command);
            return Ok();
        }
    }
}
