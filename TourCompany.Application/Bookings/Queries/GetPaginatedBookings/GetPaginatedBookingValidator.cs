using FluentValidation;

namespace TourCompany.Application.Bookings.Queries.GetPaginatedBookings
{
    public class GetPaginatedBookingsQueryValidator : AbstractValidator<GetPaginatedBookingsQuery>
    {
        public GetPaginatedBookingsQueryValidator()
        {
            RuleFor(a => a.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(a => a.PageSize).GreaterThanOrEqualTo(1);
        }
    }
}
