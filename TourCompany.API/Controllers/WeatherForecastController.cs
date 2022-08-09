using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TourCompany.Application.Common.Interfaces;

namespace TourCompany.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ITourCompanyDbContext _context;
        private readonly IMapper _mapper;
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ITourCompanyDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            //var q = from g in _context.Guides
            //        where g.GuideID == 16
            //        select new GuideVm()
            //        {
            //            Name = g.Name,
            //            Surname = g.Surname,
            //            TelephoneNumber = g.TelephoneNumber,
            //            Gender = g.Gender,
            //            Languages = (from gl in _context.GuideLanguages
            //                         join l in _context.Languages on gl.LanguageID equals l.LanguageID
            //                         where gl.GuideID == g.GuideID
            //                         select new LanguageDto()
            //                         {
            //                             LanguageID = gl.LanguageID,
            //                             Name = l.Name
            //                         }).ToList()
            //        };
            //var q2 = _context.Guides.Where(g => g.GuideID == 16).Include(g => g.Languages).ThenInclude(gl => gl.Language).Select(g => new GuideVm()
            //{
            //    Gender = g.Gender,
            //    Name = g.Name,
            //    Surname = g.Surname,
            //    TelephoneNumber = g.TelephoneNumber,
            //    Languages = g.Languages.Select(gl => new LanguageDto()
            //    {
            //        LanguageID = gl.LanguageID,
            //        Name = gl.Language.Name
            //    })
            //}).FirstOrDefault();

            //var q3 = _context.Guides.Where(g => g.GuideID == 16).Include("Languages.Language").Select(g => new GuideVm()
            //{
            //    Gender = g.Gender,
            //    Name = g.Name,
            //    Surname = g.Surname,
            //    TelephoneNumber = g.TelephoneNumber,
            //    Languages = g.Languages.Select(gl => new LanguageDto()
            //    {
            //        LanguageID = gl.LanguageID,
            //        Name = gl.Language.Name
            //    })
            //}).FirstOrDefault();
            //var c = q.FirstOrDefault();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}