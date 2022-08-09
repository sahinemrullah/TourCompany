using MediatR;
using TourCompany.Application.Common.Interfaces;
using TourCompany.Domain.Entities;

namespace TourCompany.Application.Tours.Commands.InsertTour
{
    public class InsertTourCommand : IRequest<Tour>
    {
        public string Name { get; set; } = null!;
        public IEnumerable<int> Destinations { get; set; } = null!;
    }
    public class InsertTourCommandHandler : IRequestHandler<InsertTourCommand, Tour>
    {
        private readonly ITourCompanyDbContext _context;

        public InsertTourCommandHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<Tour> Handle(InsertTourCommand request, CancellationToken cancellationToken)
        {
            var entity = new Tour()
            {
                Name = request.Name,
                Destinations = request.Destinations
                                            .Select(destinationID => new TourDestination()
                                            {
                                                DestinationID = destinationID
                                            })
                                            .ToList()
            };
            var entry = _context.Tours.Add(entity);
            await _context.Instance.SaveChangesAsync(cancellationToken);
            return entry.Entity;
        }
    }
}
