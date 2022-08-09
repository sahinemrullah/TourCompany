using FluentValidation;

namespace TourCompany.Application.Bookings.Commands.UpdateBooking
{
    public class UpdateBookingValidator : AbstractValidator<UpdateBookingCommand>
    {
        public UpdateBookingValidator()
        {
            RuleFor(b => b.Tourists).Must(t => t != null && t.Any()).WithMessage("You need to select at least 1 tourist.");

            RuleFor(b => b.TourID)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(int.MaxValue);

            RuleFor(b => b.GuideID)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(int.MaxValue);

            RuleFor(b => b.TourID)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(int.MaxValue);
        }
    }
}
