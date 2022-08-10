using MediatR;
using Microsoft.EntityFrameworkCore;
using TourCompany.Application.Common.DataTransferObjects;
using TourCompany.Application.Common.Interfaces;

namespace TourCompany.Application.Bookings.Queries.GetBooking
{
    public class GetBookingQuery : IRequest<BookingVm>
    {
        public int BookingID { get; set; }
    }
    public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, BookingVm?>
    {
        private readonly ITourCompanyDbContext _context;

        public GetBookingQueryHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<BookingVm?> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {

            var query = _context.Bookings
                .Where(d => d.ID == request.BookingID)
                .Select(b => new BookingVm
                {
                    BookingID = b.ID,
                    Date = b.Date,
                    Guide = new GuideDto()
                    {
                        GuideID = b.Guide.ID,
                        Name = b.Guide.Name,
                        Surname = b.Guide.Surname,
                        TelephoneNumber = b.Guide.TelephoneNumber
                    },
                    Tour = new TourDto()
                    {
                        TourID = b.Tour.ID,
                        Name = b.Tour.Name
                    },
                    Tourists = b.Tourists.Select(t => new TouristDto()
                    {
                        Name = t.Tourist.Name,
                        BirthDate = t.Tourist.BirthDate,
                        Gender = new GenderDto()
                        {
                            GenderID = t.Tourist.GenderID,
                            Name = t.Tourist.Gender.Name
                        },
                        Country = t.Tourist.Country.Name,
                        Nationality = t.Tourist.Nationality.Name,
                        Surname = t.Tourist.Surname,
                        TouristID = t.Tourist.ID
                    })
                });
            return await query.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
