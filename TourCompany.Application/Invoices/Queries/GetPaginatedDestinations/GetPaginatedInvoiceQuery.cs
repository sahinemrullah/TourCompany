using MediatR;
using Microsoft.EntityFrameworkCore;
using TourCompany.Application.Common.DataTransferObjects;
using TourCompany.Application.Common.Interfaces;

namespace TourCompany.Application.Invoices.Queries.GetPaginatedInvoices
{
    public class GetPaginatedInvoicesQuery : IRequest<PaginatedList<InvoiceDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Filter { get; set; }
    }
    public class GetPaginatedInvoicesQueryHandler : IRequestHandler<GetPaginatedInvoicesQuery, PaginatedList<InvoiceDto>>
    {
        private readonly ITourCompanyDbContext _context;

        public GetPaginatedInvoicesQueryHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<InvoiceDto>> Handle(GetPaginatedInvoicesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Invoices.AsQueryable();
            if (request.Filter != null)
                query = query.Where(i => i.InvoiceNo.Contains(request.Filter));
            var items = await query
                            .OrderBy(d => d.InvoiceID)
                            .Skip((request.PageNumber - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .Select(i => new InvoiceDto
                            {
                                InvoiceID = i.InvoiceID,
                                InvoiceNo = i.InvoiceNo,
                                TouristID = i.TouristID,
                                Tourist = $"{i.Tourist.Name} {i.Tourist.Surname}",
                                Date = i.Date,
                                TotalPrice = i.InvoiceItems.Sum(it => it.Price)
                            })
                            .ToListAsync(cancellationToken);
            var count = await _context.Invoices
                    .CountAsync(cancellationToken);
            return new PaginatedList<InvoiceDto>()
            {
                Items = items,
                Count = count,
            };
        }
    }
}
