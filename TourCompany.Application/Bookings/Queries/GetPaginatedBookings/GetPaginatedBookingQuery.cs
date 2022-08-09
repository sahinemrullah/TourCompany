using MediatR;
using Microsoft.EntityFrameworkCore;
using TourCompany.Application.Common.DataTransferObjects;
using TourCompany.Application.Common.Interfaces;

namespace TourCompany.Application.Bookings.Queries.GetPaginatedBookings
{
    public class GetPaginatedBookingsQuery : IRequest<PaginatedList<BookingVm>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetPaginatedBookingsQueryHandler : IRequestHandler<GetPaginatedBookingsQuery, PaginatedList<BookingVm>>
    {
        private readonly ITourCompanyDbContext _context;

        public GetPaginatedBookingsQueryHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<BookingVm>> Handle(GetPaginatedBookingsQuery request, CancellationToken cancellationToken)
        {
            var items = await _context.Bookings
                            .Skip((request.PageNumber - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .Select(b => new BookingVm
                            {
                                BookingID = b.BookingID,
                                Date = b.Date,
                                Guide = new GuideDto()
                                {
                                    GuideID = b.Guide.GuideID,
                                    Name = b.Guide.Name,
                                    Surname = b.Guide.Surname,
                                    TelephoneNumber = b.Guide.TelephoneNumber
                                },
                                Tour = new TourDto()
                                {
                                    TourID = b.Tour.TourID,
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
                                    TouristID = t.Tourist.TouristID
                                })
                            })
                            .ToListAsync(cancellationToken);
            var count = await _context.Bookings
                    .CountAsync(cancellationToken);
            return new PaginatedList<BookingVm>()
            {
                Items = items,
                Count = count,
            };
        }
    }
}
