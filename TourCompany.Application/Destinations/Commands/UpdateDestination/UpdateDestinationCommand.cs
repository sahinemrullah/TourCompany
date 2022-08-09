using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TourCompany.Application.Common.Exceptions;
using TourCompany.Application.Common.Interfaces;
using TourCompany.Domain.Entities;

namespace TourCompany.Application.Destinations.Commands.UpdateDestination
{
    public class UpdateDestinationCommand : IRequest<Unit>
    {
        public int DestinationID { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
    }
    public class UpdateDestinationCommandHandler : IRequestHandler<UpdateDestinationCommand, Unit>
    {
        private readonly ITourCompanyDbContext _context;

        public UpdateDestinationCommandHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateDestinationCommand request, CancellationToken cancellationToken)
        {
            var entity = _context.Destinations.Find(request.DestinationID);
            if (entity == null)
                throw new NotFoundException(typeof(Destination).Name, request.DestinationID);
            EntityEntry<Destination> entry = _context.Instance.Entry(entity);
            entry.CurrentValues.SetValues(request);
            await _context.Instance.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
