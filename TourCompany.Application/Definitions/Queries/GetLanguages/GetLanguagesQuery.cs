using MediatR;
using Microsoft.EntityFrameworkCore;
using TourCompany.Application.Common.DataTransferObjects;
using TourCompany.Application.Common.Interfaces;

namespace TourCompany.Application.Definitions.Queries.GetLanguages
{
    public class GetLanguagesQuery : IRequest<IEnumerable<LanguageDto>>
    {

    }
    public class GetLanguagesQueryHandler : IRequestHandler<GetLanguagesQuery, IEnumerable<LanguageDto>>
    {
        private readonly ITourCompanyDbContext _context;

        public GetLanguagesQueryHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LanguageDto>> Handle(GetLanguagesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Languages
                            .Select(l => new LanguageDto()
                            {
                                LanguageID = l.LanguageID,
                                Name = l.Name,
                            })
                            .ToListAsync(cancellationToken);
        }
    }
}
