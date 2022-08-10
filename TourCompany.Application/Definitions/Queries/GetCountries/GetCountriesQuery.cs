using MediatR;
using Microsoft.EntityFrameworkCore;
using TourCompany.Application.Common.DataTransferObjects;
using TourCompany.Application.Common.Interfaces;

namespace TourCompany.Application.Definitions.Queries.GetCountries
{
    public class GetCountriesQuery : IRequest<IEnumerable<CountryDto>>
    {

    }
    public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, IEnumerable<CountryDto>>
    {
        private readonly ITourCompanyDbContext _context;

        public GetCountriesQueryHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CountryDto>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Countries
                            .Select(c => new CountryDto()
                            {
                                CountryID = c.ID,
                                Name = c.Name,
                            })
                            .ToListAsync(cancellationToken);
        }
    }
}
