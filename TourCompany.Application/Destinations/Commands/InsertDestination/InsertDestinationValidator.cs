using FluentValidation;

namespace TourCompany.Application.Destinations.Commands.InsertDestination
{
    public class InsertDestinationValidator : AbstractValidator<InsertDestinationCommand>
    {
        public InsertDestinationValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(d => d.Price)
                .GreaterThanOrEqualTo(20.0m);
        }
    }
}
