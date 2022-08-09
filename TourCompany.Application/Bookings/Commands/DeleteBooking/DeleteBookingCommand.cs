using MediatR;
using TourCompany.Application.Common.Exceptions;
using TourCompany.Application.Common.Interfaces;
using TourCompany.Domain.Entities;

namespace TourCompany.Application.Bookings.Commands.DeleteBooking
{
    public class DeleteBookingCommand : IRequest<Unit>
    {
        public int BookingID { get; set; }
    }
    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, Unit>
    {
        private readonly ITourCompanyDbContext _context;

        public DeleteBookingCommandHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            var entity = _context.Bookings.Find(request.BookingID);
            if (entity == null)
                throw new NotFoundException(typeof(Booking).Name, request.BookingID);
            var entry = _context.Instance.Entry(entity);
            await entry.Collection(e => e.Tourists).LoadAsync(cancellationToken);
            if (entry.Entity.Tourists.Any())
                throw new InvalidOperationException();
            _context.Bookings.Remove(entity);
            await _context.Instance.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
