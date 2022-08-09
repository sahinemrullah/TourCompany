using FluentValidation;

namespace TourCompany.Application.Destinations.Commands.DeleteDestination
{
    public class DeleteDestinationCommandValidator : AbstractValidator<DeleteDestinationCommand>
    {
        public DeleteDestinationCommandValidator()
        {
            RuleFor(d => d.DestinationID)
                .GreaterThanOrEqualTo(1);
        }
    }
}
