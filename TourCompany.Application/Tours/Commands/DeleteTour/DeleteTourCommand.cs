using MediatR;
using TourCompany.Application.Common.Exceptions;
using TourCompany.Application.Common.Interfaces;
using TourCompany.Domain.Entities;

namespace TourCompany.Application.Tours.Commands.DeleteTour
{
    public class DeleteTourCommand : IRequest<Unit>
    {
        public int TourID { get; set; }
    }
    public class DeleteTourCommandHandler : IRequestHandler<DeleteTourCommand, Unit>
    {
        private readonly ITourCompanyDbContext _context;

        public DeleteTourCommandHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTourCommand request, CancellationToken cancellationToken)
        {
            var hasBooking = (from b in _context.Bookings
                              where b.TourID == request.TourID
                              select b.BookingID)
                              .Any();

            if (hasBooking)
                throw new BookingConflictException();

            var entity = _context.Tours.Find(request.TourID);
            if (entity == null)
                throw new NotFoundException(typeof(Tour).Name, request.TourID);
            _context.Tours.Remove(entity);
            await _context.Instance.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
