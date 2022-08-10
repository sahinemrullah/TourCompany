using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TourCompany.Application.Common.Exceptions;
using TourCompany.Application.Common.Interfaces;
using TourCompany.Domain.Entities;

namespace TourCompany.Application.Tours.Commands.UpdateTour
{
    public class UpdateTourCommand : IRequest<int>
    {
        public int TourID { get; set; }
        public string Name { get; set; } = null!;
        public IEnumerable<int> Destinations { get; set; } = null!;
    }
    public class UpdateTourCommandHandler : IRequestHandler<UpdateTourCommand, int>
    {
        private readonly ITourCompanyDbContext _context;

        public UpdateTourCommandHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateTourCommand request, CancellationToken cancellationToken)
        {
            var hasBooking = (from b in _context.Bookings
                              where b.TourID == request.TourID
                              select b.ID)
                              .Any();

            if (hasBooking)
                throw new BookingConflictException();

            var tour = await _context.Tours
                                     .Where(g => g.ID == request.TourID)
                                     .FirstOrDefaultAsync(cancellationToken);

            if (tour == null)
                throw new NotFoundException(nameof(Tour), request.TourID);

            EntityEntry<Tour> entry = _context.Instance.Entry(tour);
            entry.CurrentValues
                 .SetValues(request);

            entry.Collection(t => t.Destinations)
                 .Load();

            var oldDestinations = tour.Destinations
                                      .AsQueryable();

            var newDestinations = request.Destinations
                                         .AsQueryable();

            var destinationsToBeDeleted = oldDestinations
                                        .ExceptBy(newDestinations, gl => gl.DestinationID);

            var destinationsToBeInserted = newDestinations
                                            .Except(oldDestinations
                                                        .Select(l => l.DestinationID))
                                            .Select(l => new TourDestination()
                                            {
                                                TourID = request.TourID,
                                                DestinationID = l
                                            });
            _context.TourDestinations
                    .RemoveRange(destinationsToBeDeleted);

            _context.TourDestinations
                    .AddRange(destinationsToBeInserted);

            await _context.Instance
                          .SaveChangesAsync(cancellationToken);

            return tour.ID;
        }
    }
}
