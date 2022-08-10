using MediatR;
using Microsoft.EntityFrameworkCore;
using TourCompany.Application.Common.DataTransferObjects;
using TourCompany.Application.Common.Interfaces;

namespace TourCompany.Application.Guides.Queries.GetPaginatedGuides
{
    public class GetPaginatedGuidesQuery : IRequest<PaginatedList<GuideVm>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetPaginatedGuidesQueryHandler : IRequestHandler<GetPaginatedGuidesQuery, PaginatedList<GuideVm>>
    {
        private readonly ITourCompanyDbContext _context;

        public GetPaginatedGuidesQueryHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<GuideVm>> Handle(GetPaginatedGuidesQuery request, CancellationToken cancellationToken)
        {
            var items = await _context.Guides
                            .Where(g => g.IsActive)
                            .Skip((request.PageNumber - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .Select(g => new GuideVm
                            {
                                Gender = new GenderDto()
                                {
                                    GenderID = g.GenderID,
                                    Name = g.Gender.Name
                                },
                                GuideID = g.ID,
                                Name = g.Name,
                                Surname = g.Surname,
                                TelephoneNumber = g.TelephoneNumber,
                                Languages = g.Languages.Select(gl => new LanguageDto()
                                {
                                    LanguageID = gl.LanguageID,
                                    Name = gl.Language.Name
                                })
                            })
                            .ToListAsync(cancellationToken);
            var count = await _context.Guides
                    .CountAsync(cancellationToken);
            return new PaginatedList<GuideVm>()
            {
                Items = items,
                Count = count,
            };
        }
    }
}
