using FluentValidation;

namespace TourCompany.Application.Tourists.Queries.GetTourist
{
    public class GetTouristQueryValidator : AbstractValidator<GetTouristQuery>
    {
        public GetTouristQueryValidator()
        {
            RuleFor(e => e.TouristID)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(int.MaxValue);
        }
    }
}
