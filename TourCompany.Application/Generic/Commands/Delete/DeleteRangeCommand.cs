using MediatR;
using TourCompany.Application.Common.Interfaces;

namespace TourCompany.Application.Generic.Commands.Delete
{
    public class DeleteRangeRequest<TEntity> : IRequest<Unit> where TEntity : class
    {
        public DeleteRangeRequest(params TEntity[] entities)
        {
            Entities = entities;
        }
        public DeleteRangeRequest(IEnumerable<TEntity> entities)
        {
            Entities = entities;
        }
        public IEnumerable<TEntity> Entities { get; set; }
    }
    public class DeleteRangeCommand<TEntity> : IRequestHandler<DeleteRangeRequest<TEntity>, Unit> where TEntity : class
    {
        private readonly ITourCompanyDbContext _context;

        public DeleteRangeCommand(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteRangeRequest<TEntity> request, CancellationToken cancellationToken)
        {
            var set = _context.Instance.Set<TEntity>();
            set.RemoveRange(request.Entities);
            await _context.Instance.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
