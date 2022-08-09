using MediatR;
using TourCompany.Application.Common.Interfaces;
using TourCompany.Domain.Entities;

namespace TourCompany.Application.Destinations.Commands.InsertDestination
{
    public class InsertDestinationCommand : IRequest<Destination>
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
    }
    public class InsertDestinationCommandHandler : IRequestHandler<InsertDestinationCommand, Destination>
    {
        private readonly ITourCompanyDbContext _context;

        public InsertDestinationCommandHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<Destination> Handle(InsertDestinationCommand request, CancellationToken cancellationToken)
        {
            var entity = new Destination
            {
                Name = request.Name,
                Price = request.Price
            };
            var entry = _context.Destinations.Add(entity);
            await _context.Instance.SaveChangesAsync(cancellationToken);
            return entry.Entity;
        }
    }
}
