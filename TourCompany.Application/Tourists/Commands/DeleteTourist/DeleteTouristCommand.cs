using MediatR;
using TourCompany.Application.Common.Exceptions;
using TourCompany.Application.Common.Interfaces;
using TourCompany.Domain.Entities;

namespace TourCompany.Application.Tourists.Commands.DeleteTourist
{
    public class DeleteTouristCommand : IRequest<Unit>
    {
        public int TouristID { get; set; }
    }
    public class DeleteTouristCommandHandler : IRequestHandler<DeleteTouristCommand, Unit>
    {
        private readonly ITourCompanyDbContext _context;

        public DeleteTouristCommandHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTouristCommand request, CancellationToken cancellationToken)
        {
            var entity = _context.Tourists.Find(request.TouristID);
            if (entity == null)
                throw new NotFoundException(typeof(Tourist).Name, request.TouristID);
            _context.Tourists.Remove(entity);
            await _context.Instance.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
