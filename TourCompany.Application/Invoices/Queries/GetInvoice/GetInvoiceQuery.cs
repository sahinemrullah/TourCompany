using MediatR;
using Microsoft.EntityFrameworkCore;
using TourCompany.Application.Common.DataTransferObjects;
using TourCompany.Application.Common.Interfaces;

namespace TourCompany.Application.Invoices.Queries.GetInvoice
{
    public class GetInvoiceQuery : IRequest<InvoiceVm>
    {
        public string InvoiceNo { get; set; } = null!;
    }
    public class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, InvoiceVm?>
    {
        private readonly ITourCompanyDbContext _context;

        public GetInvoiceQueryHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<InvoiceVm?> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {
            var query = from i in _context.Invoices
                        join t in _context.Tourists
                            on i.TouristID equals t.ID
                        where i.InvoiceNo == request.InvoiceNo
                        select new InvoiceVm()
                        {
                            InvoiceID = i.ID,
                            InvoiceNo = i.InvoiceNo,
                            TouristID = i.TouristID,
                            Tourist = $"{t.Name} {t.Surname}",
                            Date = i.Date,
                            Items = (from it in _context.InvoiceItems
                                     join b in (from b in _context.Bookings
                                                join to in _context.Tours
                                                 on b.TourID equals to.ID
                                                select new
                                                {
                                                    b.ID,
                                                    to.Name
                                                })
                                         on it.BookingID equals b.BookingID
                                     where it.InvoiceID == i.ID
                                     select new InvoiceItemDto()
                                     {
                                         Price = it.Price,
                                         Tour = b.Name
                                     }).ToList()
                        };
            return await query.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
