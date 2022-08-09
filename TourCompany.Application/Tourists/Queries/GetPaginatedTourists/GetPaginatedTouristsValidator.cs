using FluentValidation;

namespace TourCompany.Application.Tourists.Queries.GetPaginatedTourists
{
    public class GetPaginatedTouristsQueryValidator : AbstractValidator<GetPaginatedTouristsQuery>
    {
        public GetPaginatedTouristsQueryValidator()
        {
            RuleFor(a => a.PageNumber)
                .GreaterThanOrEqualTo(1);
            RuleFor(a => a.PageSize)
                .GreaterThanOrEqualTo(1);
        }
    }
}
