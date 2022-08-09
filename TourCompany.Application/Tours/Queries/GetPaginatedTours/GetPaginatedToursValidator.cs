using FluentValidation;

namespace TourCompany.Application.Tours.Queries.GetPaginatedTours
{
    public class GetPaginatedToursQueryValidator : AbstractValidator<GetPaginatedToursQuery>
    {
        public GetPaginatedToursQueryValidator()
        {
            RuleFor(a => a.PageNumber)
                .GreaterThanOrEqualTo(1);
            RuleFor(a => a.PageSize)
                .GreaterThanOrEqualTo(1);
        }
    }
}
