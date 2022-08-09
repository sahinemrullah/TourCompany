using FluentValidation;

namespace TourCompany.Application.Invoices.Queries.GetPaginatedInvoices
{
    public class GetPaginatedInvoicesQueryValidator : AbstractValidator<GetPaginatedInvoicesQuery>
    {
        public GetPaginatedInvoicesQueryValidator()
        {
            RuleFor(a => a.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(a => a.PageSize).GreaterThanOrEqualTo(1);
        }
    }
}
