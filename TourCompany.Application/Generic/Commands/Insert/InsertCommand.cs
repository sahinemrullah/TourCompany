using MediatR;
using TourCompany.Application.Common.Interfaces;

namespace TourCompany.Application.Generic.Commands.Insert
{
    public class InsertRequest<TEntity, TDto> : IRequest<TEntity> where TDto : class where TEntity : class
    {
        public InsertRequest(TDto entity, Func<TDto, TEntity> mapFunc)
        {
            Entity = entity;
            MapFunc = mapFunc;
        }

        public TDto Entity { get; set; }
        public Func<TDto, TEntity> MapFunc { get; set; }
    }
    public class InsertCommand<TEntity, TDto> : IRequestHandler<InsertRequest<TEntity, TDto>, TEntity> where TEntity : class where TDto : class
    {
        private readonly ITourCompanyDbContext _context;

        public InsertCommand(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> Handle(InsertRequest<TEntity, TDto> request, CancellationToken cancellationToken)
        {
            TEntity entity = request.MapFunc(request.Entity);
            var set = _context.Instance.Set<TEntity>();
            var entry = set.Add(entity);
            await _context.Instance.SaveChangesAsync(cancellationToken);
            return entry.Entity;
        }
    }
    public class InsertRequest<TEntity> : IRequest<TEntity> where TEntity : class
    {
        public InsertRequest(TEntity entity)
        {
            Entity = entity;
        }
        public TEntity Entity { get; set; }
    }

    public class InsertCommand<TEntity> : IRequestHandler<InsertRequest<TEntity>, TEntity> where TEntity : class
    {
        private readonly ITourCompanyDbContext _context;

        public InsertCommand(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> Handle(InsertRequest<TEntity> request, CancellationToken cancellationToken)
        {
            var set = _context.Instance.Set<TEntity>();
            var entry = set.Add(request.Entity);
            await _context.Instance.SaveChangesAsync(cancellationToken);
            return entry.Entity;
        }
    }
}
