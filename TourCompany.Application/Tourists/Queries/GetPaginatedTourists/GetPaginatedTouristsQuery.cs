using MediatR;
using Microsoft.EntityFrameworkCore;
using TourCompany.Application.Common.DataTransferObjects;
using TourCompany.Application.Common.Interfaces;

namespace TourCompany.Application.Tourists.Queries.GetPaginatedTourists
{
    public class GetPaginatedTouristsQuery : IRequest<PaginatedList<TouristVm>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetPaginatedTouristsQueryHandler : IRequestHandler<GetPaginatedTouristsQuery, PaginatedList<TouristVm>>
    {
        private readonly ITourCompanyDbContext _context;

        public GetPaginatedTouristsQueryHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<TouristVm>> Handle(GetPaginatedTouristsQuery request, CancellationToken cancellationToken)
        {
            var items = await _context.Tourists
                            .Skip((request.PageNumber - 1) * request.PageSize)
                            .Take(request.PageSize)
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
                            })
                            .ToListAsync(cancellationToken);
            var count = await _context.Tourists
                    .CountAsync(cancellationToken);
            return new PaginatedList<TouristVm>()
            {
                Items = items,
                Count = count,
            };
        }
    }
}
