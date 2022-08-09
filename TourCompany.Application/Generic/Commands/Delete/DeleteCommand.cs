using MediatR;
using TourCompany.Application.Common.Exceptions;
using TourCompany.Application.Common.Interfaces;

namespace TourCompany.Application.Generic.Commands.Delete
{
    public class DeleteRequest<TEntity> : IRequest<Unit> where TEntity : class
    {
        public DeleteRequest(params object[] keys)
        {
            PrimaryKeys = keys;
        }
        public IEnumerable<object> PrimaryKeys { get; set; }
    }
    public class DeleteCommand<TEntity> : IRequestHandler<DeleteRequest<TEntity>, Unit> where TEntity : class
    {
        private readonly ITourCompanyDbContext _context;

        public DeleteCommand(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteRequest<TEntity> request, CancellationToken cancellationToken)
        {
            var set = _context.Instance.Set<TEntity>();
            var entity = set.Find(request.PrimaryKeys);
            if (entity == null)
                throw new NotFoundException(typeof(TEntity).Name, request.PrimaryKeys);
            set.Remove(entity);
            await _context.Instance.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
