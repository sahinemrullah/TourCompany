using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourCompany.Application.Common.Interfaces;

namespace TourCompany.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly ITourCompanyDbContext _context;

        public DashboardController(ITourCompanyDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 1. En çok gezilen yer/yerler neresidir?
        /// </summary>
        /// <param name="most"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult VisitedPlaces(bool most = true)
        {
            #region Not
            // Hocam burada group by a d.Name vermemin sebebi aşağıda resultta tekrar bölge ismi için join yapmamak için kullandım.
            // Join mi daha iyi yoksa group by mı hangisi daha iyi olur?
            #endregion
            var visitedPlacesCountQuery = from td in _context.TourDestinations
                                          join d in _context.Destinations
                                              on td.DestinationID equals d.ID
                                          join b in _context.Bookings
                                              on td.TourID equals b.TourID
                                          group td.DestinationID by new { td.DestinationID, d.Name } into g
                                          select new
                                          {
                                              g.Key.DestinationID,
                                              g.Key.Name,
                                              Count = g.Count()
                                          };
            var result = from r in visitedPlacesCountQuery
                         where r.Count == (most ? visitedPlacesCountQuery.Max(a => a.Count) : visitedPlacesCountQuery.Min(a => a.Count))
                         select new
                         {
                             r.DestinationID,
                             r.Name,
                             r.Count
                         };

            return Ok(result);
        }
        /// <summary>
        /// 2. Ağustos ayında en çok çalışan rehber/rehberler kimdir/kimlerdir?
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult HardestWorkingGuidesByYear(int month = 8)
        {
            var guideBookingsByYearQuery = from b in _context.Bookings
                                           group b by new { b.Date.Year, b.Date.Month, b.GuideID } into g
                                           where g.Key.Month == month
                                           select new
                                           {
                                               g.Key.Year,
                                               g.Key.GuideID,
                                               BookingCount = g.Select(a => a.GuideID).Count()
                                           };

            var maxBookingByYearQuery = from r in guideBookingsByYearQuery
                                        group r by r.Year into g
                                        select new
                                        {
                                            Year = g.Key,
                                            MaxBookingCount = g.Max(a => a.BookingCount)
                                        };

            var result = from r in guideBookingsByYearQuery
                         join m in maxBookingByYearQuery
                            on r.Year equals m.Year
                         join g in _context.Guides
                            on r.GuideID equals g.ID
                         where r.BookingCount == m.MaxBookingCount
                         select new
                         {
                             r.Year,
                             r.GuideID,
                             FullName = $"{g.Name} {g.Surname}",
                             r.BookingCount
                         };

            return Ok(result.OrderBy(r => r.Year));
        }
        /// <summary>
        /// 3. Kadın turistlerin gezdiği yerleri, toplam ziyaret edilme sayılarıyla beraber listeleyin
        /// </summary>
        /// <param name="genderName"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult TotalVisitCountByGender(string? genderName = "Kadın")
        {
            var subQuery = from tp in _context.TourParticipants
                           join t in _context.Tourists
                            on tp.TouristID equals t.ID
                           join b in _context.Bookings
                            on tp.BookingID equals b.ID
                           where t.GenderID == (from g in _context.Genders
                                                where g.Name == genderName
                                                select g.GenderID).First()
                           select new
                           {
                               b.ID,
                               b.TourID,
                           };

            var result = from td in _context.TourDestinations
                         join d in _context.Destinations
                             on td.DestinationID equals d.ID
                         join b in subQuery
                             on td.TourID equals b.TourID
                         group td.DestinationID by new { td.DestinationID, d.Name } into g
                         select new
                         {
                             g.Key.DestinationID,
                             g.Key.Name,
                             Count = g.Count()
                         };

            return Ok(result.OrderByDescending(r => r.Count));
        }
        /// <summary>
        /// 4. İngiltere’den gelip de Kız Kulesi’ni gezen turistler kimlerdir?
        /// </summary>
        /// <param name="countryName"></param>
        /// <param name="destinationName"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult TouristsVisited(string? countryName = "United Kingdom", string? destinationName = "Kız Kulesi")
        {
            var touristsSubQuery = from tourist in _context.Tourists
                                   join c in _context.Countries
                                        on tourist.CountryID equals c.ID
                                   where c.Name == countryName
                                   select new
                                   {
                                       tourist.ID,
                                       FullName = $"{tourist.Name} {tourist.Surname}",
                                   };

            var destinationSubQuery = from b in _context.Bookings
                                      join t in (from td in _context.TourDestinations
                                                 join d in _context.Destinations
                                                    on td.DestinationID equals d.ID
                                                 join t in _context.Tours
                                                    on td.TourID equals t.ID
                                                 where d.Name == destinationName
                                                 select new { t.ID })
                                      on b.TourID equals t.TourID
                                      select new { b.ID };



            var result = from tp in _context.TourParticipants
                         join t in touristsSubQuery
                          on tp.TouristID equals t.TouristID
                         join b in destinationSubQuery
                         on tp.BookingID equals b.BookingID
                         select new
                         {
                             b.BookingID,
                             t.TouristID,
                             t.FullName
                         };

            return Ok(result);
        }
        /// <summary>
        /// 5. Gezilen yerler hangi yılda kaç defa gezilmiştir?
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult VisitedPlacesSummaryBy(int year)
        {
            var tourDestionsQuery = from td in _context.TourDestinations
                                    join t in _context.Tours
                                    on td.TourID equals t.ID
                                    join d in _context.Destinations
                                    on td.DestinationID equals d.ID
                                    select new
                                    {
                                        t.ID,
                                        td.DestinationID,
                                        d.Name
                                    };
            var result = from b in _context.Bookings
                         join td in tourDestionsQuery
                         on b.TourID equals td.TourID
                         group td by new { b.Date.Year, td.DestinationID, td.Name } into g
                         where g.Key.Year == year
                         select new
                         {
                             g.Key.DestinationID,
                             g.Key.Name,
                             VisitedCount = g.Count(),
                         };
            return Ok(result.OrderByDescending(r => r.VisitedCount));
        }
        /// <summary>
        /// 6. 2’den fazla tura rehberlik eden rehberlerin en çok tanıttıkları yerler nelerdir?
        /// </summary>
        /// <param name="tourCount"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult MostIntroducedPlaces(int tourCount = 2)
        {
            var result = (from b in _context.Bookings
                          join gu in _context.Guides
                          on b.GuideID equals gu.ID
                          group b by new { b.GuideID, gu.Name, gu.Surname } into g
                          where g.Count() > tourCount
                          select new
                          {
                              g.Key.GuideID,
                              g.Key.Name,
                              g.Key.Surname,
                              MostIntroducedPlaces = (from subQuery in (from b in _context.Bookings
                                                                        join t in _context.TourDestinations
                                                                          on b.TourID equals t.TourID
                                                                        group b by new { b.GuideID, t.DestinationID } into g2
                                                                        where g2.Key.GuideID == g.Key.GuideID && g2.Count() == (from b in _context.Bookings
                                                                                                                                join t in _context.TourDestinations
                                                                                                                                  on b.TourID equals t.TourID
                                                                                                                                group b by new { b.GuideID, t.DestinationID } into g3
                                                                                                                                where g3.Key.GuideID == g.Key.GuideID
                                                                                                                                select g3.Count()).Max()
                                                                        select new { g2.Key.DestinationID, Count = g2.Count() }
                                                       )
                                                      join d in _context.Destinations
                                                       on subQuery.DestinationID equals d.ID
                                                      select new
                                                      {
                                                          d.ID,
                                                          d.Name,
                                                          subQuery.Count
                                                      }).ToList()
                          });
            return Ok(result);
        }
        /// <summary>
        /// 7. İngiliz turistler en çok nereyi gezmiştir?
        /// </summary>
        /// <param name="nationalityName"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult MostVisitedPlacesByNationality(string? nationalityName = "English")
        {
            var touristsSubQuery = from tourist in _context.Tourists
                                   join c in _context.Nationalities
                                        on tourist.NationalityID equals c.ID
                                   where c.Name == nationalityName
                                   select new
                                   {
                                       tourist.ID
                                   };

            var destinationsSubQuery = from b in _context.Bookings
                                       join td in (from td in _context.TourDestinations
                                                   join d in _context.Destinations
                                                   on td.DestinationID equals d.ID
                                                   select new
                                                   {
                                                       td.TourID,
                                                       d.ID,
                                                       d.Name
                                                   })
                                       on b.TourID equals td.TourID
                                       select new { b.ID, td.DestinationID, td.Name };

            var result = from tp in _context.TourParticipants
                         join t in touristsSubQuery
                          on tp.TouristID equals t.TouristID
                         join b in destinationsSubQuery
                            on tp.BookingID equals b.BookingID
                         group tp by new { b.DestinationID, b.Name } into g
                         where g.Count() == (from tp in _context.TourParticipants
                                             join t in touristsSubQuery
                                              on tp.TouristID equals t.TouristID
                                             join b in destinationsSubQuery
                                                on tp.BookingID equals b.BookingID
                                             group tp by new { b.DestinationID, b.Name } into g2
                                             select g2.Count())
                                             .Max()
                         select new
                         {
                             g.Key.DestinationID,
                             g.Key.Name,
                             Count = g.Count(),
                         };

            return Ok(result);
        }
        /// <summary>
        /// 8. Kapalı Çarşı’yı gezen en yaşlı turist kimdir?
        /// </summary>
        /// <param name="destinationName"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult VisitorByAge(string? destinationName = "Kapalı Çarşı")
        {
            var result = (from tp in _context.TourParticipants
                          join t in _context.Tourists
                             on tp.TouristID equals t.ID
                          join b in (from td in _context.TourDestinations
                                     join b in _context.Bookings
                                         on td.TourID equals b.TourID
                                     join d in _context.Destinations
                                         on td.DestinationID equals d.ID
                                     where d.Name == destinationName
                                     select new { b.ID })
                             on tp.BookingID equals b.BookingID
                          where EF.Functions.DateDiffYear(t.BirthDate, DateTime.Now) == (from tp in _context.TourParticipants
                                                                                         join t in _context.Tourists
                                                                                            on tp.TouristID equals t.ID
                                                                                         join b in (from td in _context.TourDestinations
                                                                                                    join b in _context.Bookings
                                                                                                        on td.TourID equals b.TourID
                                                                                                    join d in _context.Destinations
                                                                                                        on td.DestinationID equals d.ID
                                                                                                    where d.Name == destinationName
                                                                                                    select new { b.ID })
                                                                                            on tp.BookingID equals b.BookingID
                                                                                         group t by EF.Functions.DateDiffYear(t.BirthDate, DateTime.Now) into g
                                                                                         select g.Key)
                                                                                            .Max()
                          select new
                          {
                              FullName = $"{t.Name} {t.Surname}",
                              t.BirthDate,
                              Age = EF.Functions.DateDiffYear(t.BirthDate, DateTime.Now)
                          }
                          );
            return Ok(result);
        }
        /// <summary>
        /// 9. Yunanistan’dan gelen Finlandiyalı turistin gezdiği yerler nerelerdir?
        /// </summary>
        /// <param name="countryName"></param>
        /// <param name="nationalityName"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult VisitedPlacesByCountryAndNationality(string? countryName = "Greece", string? nationalityName = "Finnish")
        {
            var result = from tp in _context.TourParticipants
                         join t in (from t in _context.Tourists
                                    join c in _context.Countries
                                        on t.CountryID equals c.ID
                                    join n in _context.Nationalities
                                        on t.NationalityID equals n.ID
                                    where c.Name == countryName && n.Name == nationalityName
                                    select new { t.ID, t.Name, t.Surname })
                            on tp.TouristID equals t.TouristID
                         join b in (from td in _context.TourDestinations
                                    join b in _context.Bookings
                                        on td.TourID equals b.TourID
                                    join d in _context.Destinations
                                        on td.DestinationID equals d.ID
                                    select new { b.ID, b.Date, d.ID, DestinationName = d.Name })
                            on tp.BookingID equals b.BookingID
                         select new
                         {
                             b.BookingID,
                             b.Date,
                             Tourist = $"{t.Name} {t.Surname}",
                             b.DestinationName
                         };
            return Ok(result);
        }
        /// <summary>
        /// 10. Dolmabahçe Sarayı’na en son giden turistler ve rehberi listeleyin.
        /// </summary>
        /// <param name="destinationName"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetLastBooking(string? destinationName = "Dolmabahçe Sarayı")
        {
            var result = from b in (from b in _context.Bookings
                                    join g in _context.Guides
                                        on b.GuideID equals g.ID
                                    select new
                                    {
                                        b.ID,
                                        g.ID,
                                        GuideName = g.Name,
                                        GuideSurname = g.Surname,
                                        b.Date,
                                    })
                         where EF.Functions.DateDiffDay(b.Date, DateTime.Now) == (from td in _context.TourDestinations
                                                                                  join d in _context.Destinations
                                                                                     on td.DestinationID equals d.ID
                                                                                  join bo in _context.Bookings
                                                                                     on td.TourID equals bo.TourID
                                                                                  where d.Name == destinationName
                                                                                  group bo.Date by EF.Functions.DateDiffDay(bo.Date, DateTime.Now) into g
                                                                                  where g.Key > 0
                                                                                  select g.Key).Min()
                         select new
                         {
                             b.BookingID,
                             b.GuideID,
                             Guide = $"{b.GuideName} {b.GuideSurname}",
                             b.Date,
                             Tourists = (from tp in _context.TourParticipants
                                         join t in _context.Tourists
                                          on tp.TouristID equals t.ID
                                         where tp.BookingID == b.BookingID
                                         select new
                                         {
                                             t.ID,
                                             Tourist = $"{t.Name} {t.Surname}",
                                         }).ToList()
                         };

            return Ok(result.SingleOrDefault());
        }
    }
}
