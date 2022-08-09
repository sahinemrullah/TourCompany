using FluentValidation;

namespace TourCompany.Application.Tours.Commands.UpdateTour
{
    public class UpdateTourValidator : AbstractValidator<UpdateTourCommand>
    {
        public UpdateTourValidator()
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
