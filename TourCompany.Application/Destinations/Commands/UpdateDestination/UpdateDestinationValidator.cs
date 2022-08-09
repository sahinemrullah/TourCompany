using FluentValidation;

namespace TourCompany.Application.Destinations.Commands.UpdateDestination
{
    public class UpdateDestinationValidator : AbstractValidator<UpdateDestinationCommand>
    {
        public UpdateDestinationValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(d => d.Price)
                .GreaterThanOrEqualTo(20.0m);
        }
    }
}
