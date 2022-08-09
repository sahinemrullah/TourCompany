using MediatR;
using Microsoft.EntityFrameworkCore;
using TourCompany.Application.Common.Exceptions;
using TourCompany.Application.Common.Interfaces;
using TourCompany.Domain.Entities;

namespace TourCompany.Application.Destinations.Commands.DeleteDestination
{
    public class DeleteDestinationCommand : IRequest<Unit>
    {
        public int DestinationID { get; set; }
    }
    public class DeleteDestinationCommandHandler : IRequestHandler<DeleteDestinationCommand, Unit>
    {
        private readonly ITourCompanyDbContext _context;

        public DeleteDestinationCommandHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteDestinationCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Destinations
                                 .FindAsync(new object?[] { request.DestinationID }, cancellationToken);

            if (entity == null)
                throw new NotFoundException(typeof(Destination).Name, request.DestinationID);

            bool hasBooking = await (from td in _context.TourDestinations
                                     join b in _context.Bookings
                                         on td.TourID equals b.TourID
                                     where td.DestinationID == request.DestinationID
                                     select td)
                               .AnyAsync(cancellationToken);

            if (hasBooking)
                throw new BookingConflictException();

            _context.Destinations
                    .Remove(entity);

            await _context.Instance
                    .SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
