using MediatR;
using Microsoft.EntityFrameworkCore;
using TourCompany.Application.Common.DataTransferObjects;
using TourCompany.Application.Common.Interfaces;

namespace TourCompany.Application.Tours.Queries.GetPaginatedTours
{
    public class GetPaginatedToursQuery : IRequest<PaginatedList<TourDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetPaginatedToursQueryHandler : IRequestHandler<GetPaginatedToursQuery, PaginatedList<TourDto>>
    {
        private readonly ITourCompanyDbContext _context;

        public GetPaginatedToursQueryHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<TourDto>> Handle(GetPaginatedToursQuery request, CancellationToken cancellationToken)
        {
            var items = await _context.Tours
                            .Skip((request.PageNumber - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .Select(t => new TourDto
                            {
                                TourID = t.TourID,
                                Name = t.Name,
                                Destinations = t.Destinations
                                            .Select(td => new DestinationDto()
                                            {
                                                DestinationID = td.Destination.DestinationID,
                                                Name = td.Destination.Name,
                                                Price = td.Destination.Price
                                            })
                            })
                            .ToListAsync(cancellationToken);
            var count = await _context.Tours
                    .CountAsync(cancellationToken);
            return new PaginatedList<TourDto>()
            {
                Items = items,
                Count = count,
            };
        }
    }
}
