using MediatR;
using Microsoft.AspNetCore.Mvc;
using TourCompany.Application.Definitions.Queries.GetCountries;
using TourCompany.Application.Definitions.Queries.GetGenders;
using TourCompany.Application.Definitions.Queries.GetLanguages;
using TourCompany.Application.Definitions.Queries.GetNationalities;

namespace TourCompany.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefinitionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DefinitionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("languages")]
        public async Task<IActionResult> GetLanguages()
        {
            var languages = await _mediator.Send(new GetLanguagesQuery());

            return Ok(languages);
        }

        [HttpGet("nationalities")]
        public async Task<IActionResult> GetNationalities()
        {
            var nationalities = await _mediator.Send(new GetNationalitiesQuery());

            return Ok(nationalities);
        }

        [HttpGet("countries")]
        public async Task<IActionResult> GetCountries()
        {
            var countries = await _mediator.Send(new GetCountriesQuery());

            return Ok(countries);
        }

        [HttpGet("genders")]
        public async Task<IActionResult> GetGenders()
        {
            var genders = await _mediator.Send(new GetGendersQuery());

            return Ok(genders);
        }
    }
}
