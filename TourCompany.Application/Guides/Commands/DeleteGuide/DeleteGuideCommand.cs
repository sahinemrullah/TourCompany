using MediatR;
using TourCompany.Application.Common.Exceptions;
using TourCompany.Application.Common.Interfaces;
using TourCompany.Domain.Entities;

namespace TourCompany.Application.Guides.Commands.DeleteGuide
{
    public class DeleteGuideCommand : IRequest<Unit>
    {
        public int GuideID { get; set; }
    }
    public class DeleteGuideCommandHandler : IRequestHandler<DeleteGuideCommand, Unit>
    {
        private readonly ITourCompanyDbContext _context;

        public DeleteGuideCommandHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteGuideCommand request, CancellationToken cancellationToken)
        {
            var entity = _context.Guides.Find(request.GuideID);
            if (entity == null)
                throw new NotFoundException(typeof(Guide).Name, request.GuideID);
            entity.IsActive = false;
            await _context.Instance.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
