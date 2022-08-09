using FluentValidation;

namespace TourCompany.Application.Bookings.Queries.GetBooking
{
    public class GetBookingQueryValidator : AbstractValidator<GetBookingQuery>
    {
        public GetBookingQueryValidator()
        {
            RuleFor(e => e.BookingID)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(int.MaxValue);
        }
    }
}
