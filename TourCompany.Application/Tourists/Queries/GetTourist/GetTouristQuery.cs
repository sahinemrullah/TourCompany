using MediatR;
using Microsoft.EntityFrameworkCore;
using TourCompany.Application.Common.DataTransferObjects;
using TourCompany.Application.Common.Interfaces;

namespace TourCompany.Application.Tourists.Queries.GetTourist
{
    public class GetTouristQuery : IRequest<TouristVm>
    {
        public int TouristID { get; set; }
    }
    public class GetTouristQueryHandler : IRequestHandler<GetTouristQuery, TouristVm?>
    {
        private readonly ITourCompanyDbContext _context;

        public GetTouristQueryHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<TouristVm?> Handle(GetTouristQuery request, CancellationToken cancellationToken)
        {

            var query = _context.Tourists
                .Where(t => t.ID == request.TouristID)
                .Select(t => new TouristVm
                {
                    TouristID = t.ID,
                    Name = t.Name,
                    Surname = t.Surname,
                    BirthDate = t.BirthDate,
                    Gender = new GenderDto()
                    {
                        GenderID = t.GenderID,
                        Name = t.Gender.Name,
                    },
                    Country = new CountryDto()
                    {
                        CountryID = t.CountryID,
                        Name = t.Country.Name,
                    },
                    Nationality = new NationalityDto()
                    {
                        NationalityID = t.NationalityID,
                        Name = t.Nationality.Name,
                    }
                });
            return await query.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
