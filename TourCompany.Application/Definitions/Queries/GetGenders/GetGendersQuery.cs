using MediatR;
using Microsoft.EntityFrameworkCore;
using TourCompany.Application.Common.DataTransferObjects;
using TourCompany.Application.Common.Interfaces;

namespace TourCompany.Application.Definitions.Queries.GetGenders
{
    public class GetGendersQuery : IRequest<IEnumerable<GenderDto>>
    {

    }
    public class GetGendersQueryHandler : IRequestHandler<GetGendersQuery, IEnumerable<GenderDto>>
    {
        private readonly ITourCompanyDbContext _context;

        public GetGendersQueryHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GenderDto>> Handle(GetGendersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Genders
                            .Select(g => new GenderDto()
                            {
                                GenderID = g.GenderID,
                                Name = g.Name,
                            })
                            .ToListAsync(cancellationToken);
        }
    }
}
