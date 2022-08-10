using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TourCompany.Application.Common.Exceptions;
using TourCompany.Application.Common.Interfaces;
using TourCompany.Domain.Entities;

namespace TourCompany.Application.Bookings.Commands.UpdateBooking
{
    public class UpdateBookingCommand : IRequest<int>
    {
        public int BookingID { get; set; }
        public int TourID { get; set; }
        public int GuideID { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<int> Tourists { get; set; } = null!;
    }
    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, int>
    {
        private readonly ITourCompanyDbContext _context;

        public UpdateBookingCommandHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            var Booking = _context.Bookings.Where(g => g.ID == request.BookingID).FirstOrDefault();

            if (Booking == null)
                throw new NotFoundException(nameof(Booking), request.BookingID);

            EntityEntry<Booking> entry = _context.Instance.Entry(Booking);
            entry.CurrentValues.SetValues(request);

            entry.Collection(t => t.Tourists).Load();
            var oldTourists = Booking.Tourists.AsQueryable();
            var newTourists = request.Tourists.AsQueryable();
            var touristsToBeDeleted = oldTourists
                                        .ExceptBy(newTourists, gl => gl.TouristID);

            var touristsToBeInserted = newTourists
                                            .Except(oldTourists
                                                        .Select(l => l.TouristID))
                                            .Select(l => new TourParticipant()
                                            {
                                                BookingID = request.BookingID,
                                                TouristID = l
                                            });
            _context.TourParticipants.RemoveRange(touristsToBeDeleted);
            _context.TourParticipants.AddRange(touristsToBeInserted);

            await _context.Instance.SaveChangesAsync(cancellationToken);

            return Booking.ID;
        }
    }
}
