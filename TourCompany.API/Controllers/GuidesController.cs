using MediatR;
using Microsoft.AspNetCore.Mvc;
using TourCompany.Application.Guides.Commands.DeleteGuide;
using TourCompany.Application.Guides.Commands.InsertGuide;
using TourCompany.Application.Guides.Commands.UpdateGuide;
using TourCompany.Application.Guides.Queries.GetGuide;
using TourCompany.Application.Guides.Queries.GetPaginatedGuides;

namespace TourCompany.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GuidesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GuidesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var guides = await _mediator.Send(new GetPaginatedGuidesQuery()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            });
            return Ok(guides);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var guide = await _mediator.Send(new GetGuideQuery()
            {
                GuideID = id,
            });

            return Ok(guide);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] InsertGuideCommand command)
        {
            var guide = await _mediator.Send(command);
            return Ok(guide.GuideID);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] UpdateGuideCommand command)
        {
            command.GuideID = id;
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _mediator.Send(new DeleteGuideCommand() { GuideID = id });
            return Ok();
        }
    }
}
