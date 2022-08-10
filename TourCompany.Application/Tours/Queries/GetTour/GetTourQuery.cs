using MediatR;
using Microsoft.EntityFrameworkCore;
using TourCompany.Application.Common.DataTransferObjects;
using TourCompany.Application.Common.Interfaces;

namespace TourCompany.Application.Tours.Queries.GetTour
{
    public class GetTourQuery : IRequest<TourVm>
    {
        public int TourID { get; set; }
    }
    public class GetTourQueryHandler : IRequestHandler<GetTourQuery, TourVm?>
    {
        private readonly ITourCompanyDbContext _context;

        public GetTourQueryHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<TourVm?> Handle(GetTourQuery request, CancellationToken cancellationToken)
        {

            var query = _context.Tours
                .Where(t => t.ID == request.TourID)
                .Select(t => new TourVm()
                {
                    Name = t.Name,
                    Destinations = t.Destinations.Select(td => new DestinationDto()
                    {
                        DestinationID = td.DestinationID,
                        Name = td.Destination.Name,
                        Price = td.Destination.Price,
                    }),
                });
            return await query.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
