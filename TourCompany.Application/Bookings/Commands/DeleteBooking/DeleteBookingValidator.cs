using FluentValidation;

namespace TourCompany.Application.Bookings.Commands.DeleteBooking
{
    public class DeleteBookingValidator : AbstractValidator<DeleteBookingCommand>
    {
        public DeleteBookingValidator()
        {
            RuleFor(d => d.BookingID)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(int.MaxValue);
        }
    }
}
