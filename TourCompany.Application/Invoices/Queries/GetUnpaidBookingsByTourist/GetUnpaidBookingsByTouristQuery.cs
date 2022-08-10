using MediatR;
using Microsoft.EntityFrameworkCore;
using TourCompany.Application.Common.DataTransferObjects;
using TourCompany.Application.Common.Interfaces;

namespace TourCompany.Application.Invoices.Queries.GetUnpaidBookingsByTourist
{
    public class GetUnpaidBookingsByTouristQuery : IRequest<IEnumerable<UnpaidBookingDto>>
    {
        public int TouristID { get; set; }
    }
    public class GetUnpaidBookingsByTouristQueryHandler : IRequestHandler<GetUnpaidBookingsByTouristQuery, IEnumerable<UnpaidBookingDto>>
    {
        private readonly ITourCompanyDbContext _context;

        public GetUnpaidBookingsByTouristQueryHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UnpaidBookingDto>> Handle(GetUnpaidBookingsByTouristQuery request, CancellationToken cancellationToken)
        {
            var query = (from tp in _context.TourParticipants
                         join it in _context.InvoiceItems
                            on new { tp.TouristID, tp.BookingID } equals new { TouristID = it.TouristID, BookingID = it.BookingID } into invoices
                         from it in invoices.DefaultIfEmpty()
                         join b in (from b in _context.Bookings
                                    join t in _context.Tours
                                        on b.TourID equals t.ID
                                    select new { b.ID, b.Date, t.ID, t.Name })
                            on tp.BookingID equals b.BookingID

                         where it.InvoiceID == null && tp.TouristID == request.TouristID
                         select new UnpaidBookingDto()
                         {
                             BookingID = b.BookingID,
                             Name = b.Name,
                             Date = b.Date,
                         });
            return await query.ToListAsync(cancellationToken);
        }
    }
}
