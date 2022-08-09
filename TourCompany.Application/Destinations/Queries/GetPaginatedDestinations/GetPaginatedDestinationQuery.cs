using MediatR;
using Microsoft.EntityFrameworkCore;
using TourCompany.Application.Common.DataTransferObjects;
using TourCompany.Application.Common.Interfaces;

namespace TourCompany.Application.Destinations.Queries.GetPaginatedDestinations
{
    public class GetPaginatedDestinationsQuery : IRequest<PaginatedList<DestinationDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetPaginatedDestinationsQueryHandler : IRequestHandler<GetPaginatedDestinationsQuery, PaginatedList<DestinationDto>>
    {
        private readonly ITourCompanyDbContext _context;

        public GetPaginatedDestinationsQueryHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<DestinationDto>> Handle(GetPaginatedDestinationsQuery request, CancellationToken cancellationToken)
        {
            var items = await _context.Destinations
                            .OrderBy(d => d.DestinationID)
                            .Skip((request.PageNumber - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .Select(d => new DestinationDto
                            {
                                DestinationID = d.DestinationID,
                                Name = d.Name,
                                Price = d.Price,
                            })
                            .ToListAsync(cancellationToken);
            var count = await _context.Destinations
                    .CountAsync(cancellationToken);
            return new PaginatedList<DestinationDto>()
            {
                Items = items,
                Count = count,
            };
        }
    }
}
