using FluentValidation;

namespace TourCompany.Application.Tours.Commands.InsertTour
{
    public class InsertTourValidator : AbstractValidator<InsertTourCommand>
    {
        public InsertTourValidator()
        {
            RuleFor(t => t.Name)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(t => t.Destinations)
                .Must(d => d != null && d.Any() && d.Count() <= 3)
                .WithMessage("You need to select 1 to 3 destination.");
        }
    }
}
