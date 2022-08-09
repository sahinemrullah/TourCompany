using FluentValidation;

namespace TourCompany.Application.Destinations.Queries.GetDestination
{
    public class GetDestinationQueryValidator : AbstractValidator<GetDestinationQuery>
    {
        public GetDestinationQueryValidator()
        {
            RuleFor(e => e.DestinationID)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(int.MaxValue);
        }
    }
}
