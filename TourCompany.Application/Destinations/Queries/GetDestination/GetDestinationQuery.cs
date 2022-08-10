using MediatR;
using Microsoft.EntityFrameworkCore;
using TourCompany.Application.Common.DataTransferObjects;
using TourCompany.Application.Common.Interfaces;

namespace TourCompany.Application.Destinations.Queries.GetDestination
{
    public class GetDestinationQuery : IRequest<DestinationDto>
    {
        public int DestinationID { get; set; }
    }
    public class GetDestinationQueryHandler : IRequestHandler<GetDestinationQuery, DestinationDto?>
    {
        private readonly ITourCompanyDbContext _context;

        public GetDestinationQueryHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<DestinationDto?> Handle(GetDestinationQuery request, CancellationToken cancellationToken)
        {

            var query = _context.Destinations
                .Where(d => d.ID == request.DestinationID)
                .Select(d => new DestinationDto()
                {
                    DestinationID = d.ID,
                    Name = d.Name,
                    Price = d.Price,
                });
            return await query.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
