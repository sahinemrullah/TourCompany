using MediatR;
using TourCompany.Application.Common.Interfaces;
using TourCompany.Domain.Entities;

namespace TourCompany.Application.Tourists.Commands.InsertTourist
{
    public class InsertTouristCommand : IRequest<Tourist>
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public int GenderID { get; set; }
        public DateTime BirthDate { get; set; }
        public int NationalityID { get; set; }
        public int CountryID { get; set; }
    }
    public class InsertTouristCommandHandler : IRequestHandler<InsertTouristCommand, Tourist>
    {
        private readonly ITourCompanyDbContext _context;

        public InsertTouristCommandHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<Tourist> Handle(InsertTouristCommand request, CancellationToken cancellationToken)
        {
            var entity = new Tourist
            {
                Name = request.Name,
                Surname = request.Surname,
                BirthDate = request.BirthDate,
                GenderID = request.GenderID,
                CountryID = request.CountryID,
                NationalityID = request.NationalityID
            };
            var entry = _context.Tourists.Add(entity);
            await _context.Instance.SaveChangesAsync(cancellationToken);
            return entry.Entity;
        }
    }
}
