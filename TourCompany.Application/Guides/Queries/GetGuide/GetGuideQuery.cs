using MediatR;
using Microsoft.EntityFrameworkCore;
using TourCompany.Application.Common.DataTransferObjects;
using TourCompany.Application.Common.Interfaces;

namespace TourCompany.Application.Guides.Queries.GetGuide
{
    public class GetGuideQuery : IRequest<GuideVm>
    {
        public int GuideID { get; set; }
    }
    public class GetGuideQueryHandler : IRequestHandler<GetGuideQuery, GuideVm?>
    {
        private readonly ITourCompanyDbContext _context;

        public GetGuideQueryHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<GuideVm?> Handle(GetGuideQuery request, CancellationToken cancellationToken)
        {

            var query = _context.Guides
                .Where(g => g.GuideID == request.GuideID && g.IsActive)
                .Select(g => new GuideVm()
                {
                    Gender = new GenderDto()
                    {
                        GenderID = g.GenderID,
                        Name = g.Gender.Name
                    },
                    GuideID = g.GuideID,
                    Name = g.Name,
                    Surname = g.Surname,
                    TelephoneNumber = g.TelephoneNumber,
                    Languages = g.Languages
                                .Select(gl => new LanguageDto()
                                {
                                    LanguageID = gl.LanguageID,
                                    Name = gl.Language.Name
                                })
                });
            return await query.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
