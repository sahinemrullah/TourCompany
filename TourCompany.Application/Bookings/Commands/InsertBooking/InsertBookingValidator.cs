using FluentValidation;

namespace TourCompany.Application.Bookings.Commands.InsertBooking
{
    public class InsertBookingValidator : AbstractValidator<InsertBookingCommand>
    {
        public InsertBookingValidator()
        {
            RuleFor(b => b.Tourists)
                .Must(t => t != null && t.Any())
                .WithMessage("You need to select at least 1 tourist.");

            RuleFor(b => b.TourID)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(int.MaxValue)
                .WithMessage("You need to select tour guide.");

            RuleFor(b => b.GuideID)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(int.MaxValue)
                .WithMessage("You need to select valid guide.");

            RuleFor(b => b.Date)
                .GreaterThanOrEqualTo(DateTime.MinValue);
        }
    }
}
