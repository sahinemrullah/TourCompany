using FluentValidation;

namespace TourCompany.Application.Destinations.Queries.GetPaginatedDestinations
{
    public class GetPaginatedDestinationsQueryValidator : AbstractValidator<GetPaginatedDestinationsQuery>
    {
        public GetPaginatedDestinationsQueryValidator()
        {
            RuleFor(a => a.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(a => a.PageSize).GreaterThanOrEqualTo(1);
        }
    }
}
