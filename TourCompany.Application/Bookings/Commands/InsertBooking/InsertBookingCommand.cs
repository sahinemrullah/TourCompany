using MediatR;
using TourCompany.Application.Common.Interfaces;
using TourCompany.Domain.Entities;

namespace TourCompany.Application.Bookings.Commands.InsertBooking
{
    public class InsertBookingCommand : IRequest<Booking>
    {
        public DateTime Date { get; set; }
        public int GuideID { get; set; }
        public int TourID { get; set; }
        public IEnumerable<int> Tourists { get; set; } = null!;
    }
    public class InsertBookingCommandHandler : IRequestHandler<InsertBookingCommand, Booking>
    {
        private readonly ITourCompanyDbContext _context;

        public InsertBookingCommandHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<Booking> Handle(InsertBookingCommand request, CancellationToken cancellationToken)
        {
            var entity = new Booking()
            {
                TourID = request.TourID,
                Date = request.Date,
                GuideID = request.GuideID,
                Tourists = request.Tourists
                                    .Select(touristID => new TourParticipant()
                                    {
                                        TouristID = touristID,
                                    })
                                    .ToList()
            };
            var entry = _context.Bookings.Add(entity);
            await _context.Instance.SaveChangesAsync(cancellationToken);
            return entry.Entity;
        }
    }
}
