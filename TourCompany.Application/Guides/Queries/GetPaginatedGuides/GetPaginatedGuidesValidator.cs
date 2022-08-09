using FluentValidation;

namespace TourCompany.Application.Guides.Queries.GetPaginatedGuides
{
    public class GetPaginatedGuidesQueryValidator : AbstractValidator<GetPaginatedGuidesQuery>
    {
        public GetPaginatedGuidesQueryValidator()
        {
            RuleFor(a => a.PageNumber)
                .GreaterThanOrEqualTo(1);
            RuleFor(a => a.PageSize)
                .GreaterThanOrEqualTo(1);
        }
    }
}
