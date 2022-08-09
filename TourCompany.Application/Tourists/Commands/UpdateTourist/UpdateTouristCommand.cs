using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TourCompany.Application.Common.Exceptions;
using TourCompany.Application.Common.Interfaces;
using TourCompany.Domain.Entities;

namespace TourCompany.Application.Tourists.Commands.UpdateTourist
{
    public class UpdateTouristCommand : IRequest<Unit>
    {
        public int TouristID { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public int GenderID { get; set; }
        public DateTime BirthDate { get; set; }
        public int NationalityID { get; set; }
        public int CountryID { get; set; }
    }
    public class UpdateTouristCommandHandler : IRequestHandler<UpdateTouristCommand, Unit>
    {
        private readonly ITourCompanyDbContext _context;

        public UpdateTouristCommandHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTouristCommand request, CancellationToken cancellationToken)
        {
            var entity = _context.Tourists.Find(request.TouristID);
            if (entity == null)
                throw new NotFoundException(typeof(Tourist).Name, request.TouristID);
            EntityEntry<Tourist> entry = _context.Instance.Entry(entity);
            entry.CurrentValues.SetValues(request);
            await _context.Instance.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
