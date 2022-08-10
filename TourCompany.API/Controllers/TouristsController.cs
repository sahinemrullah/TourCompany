using MediatR;
using Microsoft.AspNetCore.Mvc;
using TourCompany.Application.Tourists.Commands.InsertTourist;
using TourCompany.Application.Tourists.Commands.UpdateTourist;
using TourCompany.Application.Tourists.Queries.GetPaginatedTourists;
using TourCompany.Application.Tourists.Queries.GetTourist;

namespace TourCompany.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TouristsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TouristsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var tourists = await _mediator.Send(new GetPaginatedTouristsQuery()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            });
            return Ok(tourists);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var tourist = await _mediator.Send(new GetTouristQuery()
            {
                TouristID = id,
            });

            return Ok(tourist);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] InsertTouristCommand command)
        {
            var tourist = await _mediator.Send(command);
            return Ok(tourist.ID);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] UpdateTouristCommand command)
        {
            command.TouristID = id;
            await _mediator.Send(command);
            return Ok();
        }
    }
}
