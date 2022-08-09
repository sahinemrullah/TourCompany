using FluentValidation;

namespace TourCompany.Application.Tours.Queries.GetTour
{
    public class GetTourQueryValidator : AbstractValidator<GetTourQuery>
    {
        public GetTourQueryValidator()
        {
            RuleFor(e => e.TourID)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(int.MaxValue);
        }
    }
}
