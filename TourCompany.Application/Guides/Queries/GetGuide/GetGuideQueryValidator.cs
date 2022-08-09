using FluentValidation;

namespace TourCompany.Application.Guides.Queries.GetGuide
{
    public class GetGuideQueryValidator : AbstractValidator<GetGuideQuery>
    {
        public GetGuideQueryValidator()
        {
            RuleFor(e => e.GuideID)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(int.MaxValue);
        }
    }
}
