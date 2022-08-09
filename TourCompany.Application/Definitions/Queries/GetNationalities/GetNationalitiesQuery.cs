using MediatR;
using Microsoft.EntityFrameworkCore;
using TourCompany.Application.Common.DataTransferObjects;
using TourCompany.Application.Common.Interfaces;

namespace TourCompany.Application.Definitions.Queries.GetNationalities
{
    public class GetNationalitiesQuery : IRequest<IEnumerable<NationalityDto>>
    {

    }
    public class GetNationalitiesQueryHandler : IRequestHandler<GetNationalitiesQuery, IEnumerable<NationalityDto>>
    {
        private readonly ITourCompanyDbContext _context;

        public GetNationalitiesQueryHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NationalityDto>> Handle(GetNationalitiesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Nationalities
                            .Select(n => new NationalityDto()
                            {
                                NationalityID = n.NationalityID,
                                Name = n.Name,
                            })
                            .ToListAsync(cancellationToken);
        }
    }
}
