using MediatR;
using Microsoft.EntityFrameworkCore;
using TourCompany.Application.Common.Exceptions;
using TourCompany.Application.Common.Interfaces;
using TourCompany.Domain.Entities;

namespace TourCompany.Application.Invoices.Commands.InsertInvoice
{
    public class InsertInvoiceCommand : IRequest<Invoice>
    {
        public int TouristID { get; set; }
        public IEnumerable<int> Bookings { get; set; } = null!;
    }
    public class InsertInvoiceCommandHandler : IRequestHandler<InsertInvoiceCommand, Invoice>
    {
        private readonly ITourCompanyDbContext _context;

        public InsertInvoiceCommandHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<Invoice> Handle(InsertInvoiceCommand request, CancellationToken cancellationToken)
        {
            var tourist = (from t in _context.Tourists
                           where t.ID == request.TouristID
                           select new
                           {
                               t.ID,
                               Discount = EF.Functions.DateDiffYear(DateTime.Today, t.BirthDate) > 60 ? 0.15 : 0.0
                           }).FirstOrDefault();
            if (tourist == null)
                throw new NotFoundException(nameof(Tourist), request.TouristID);
            var items = (from tp in _context.TourParticipants
                         join b in (from td in _context.TourDestinations
                                    join d in _context.Destinations
                                     on td.DestinationID equals d.ID
                                    join b in _context.Bookings
                                     on td.TourID equals b.TourID
                                    group d.Price by new { b.ID } into g
                                    select new
                                    {
                                        g.Key.BookingID,
                                        TotalPrice = g.Sum()
                                    })
                             on tp.BookingID equals b.BookingID
                         where request.Bookings.Contains(b.BookingID) && tp.TouristID == request.TouristID
                         select new InvoiceItem()
                         {
                             TouristID = tp.TouristID,
                             BookingID = b.BookingID,
                             Price = b.TotalPrice * (1.0m - Convert.ToDecimal(tourist.Discount))
                         }).ToList();
            var entity = new Invoice
            {
                Date = DateTime.Today,
                AppliedDiscount = tourist.Discount,
                TouristID = request.TouristID,
                InvoiceItems = items
            };
            var entry = _context.Invoices.Add(entity);
            await _context.Instance.SaveChangesAsync(cancellationToken);
            return entry.Entity;
        }
    }
}
