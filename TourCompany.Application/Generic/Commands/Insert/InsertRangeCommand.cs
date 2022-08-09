using MediatR;
using TourCompany.Application.Common.Interfaces;

namespace TourCompany.Application.Generic.Commands.Insert
{
    public class InsertRangeRequest<TEntity, TDto> : IRequest<Unit> where TDto : class where TEntity : class
    {
        public InsertRangeRequest(IEnumerable<TDto> entities, Func<TDto, TEntity> mapFunc)
        {
            Entities = entities;
            MapFunc = mapFunc;
        }

        public IEnumerable<TDto> Entities { get; set; }
        public Func<TDto, TEntity> MapFunc { get; set; }
    }
    public class InsertRangeCommand<TEntity, TDto> : IRequestHandler<InsertRangeRequest<TEntity, TDto>, Unit> where TEntity : class where TDto : class
    {
        private readonly ITourCompanyDbContext _context;

        public InsertRangeCommand(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(InsertRangeRequest<TEntity, TDto> request, CancellationToken cancellationToken)
        {
            IEnumerable<TEntity> entities = request.Entities.Select(request.MapFunc);
            var set = _context.Instance.Set<TEntity>();
            set.AddRange(entities);
            await _context.Instance.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

    public class InsertRangeRequest<TEntity> : IRequest<Unit> where TEntity : class
    {
        public InsertRangeRequest(IEnumerable<TEntity> entities)
        {
            Entities = entities;
        }
        public IEnumerable<TEntity> Entities { get; set; }
    }

    public class InsertRangeCommand<TEntity> : IRequestHandler<InsertRangeRequest<TEntity>, Unit> where TEntity : class
    {
        private readonly ITourCompanyDbContext _context;

        public InsertRangeCommand(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(InsertRangeRequest<TEntity> request, CancellationToken cancellationToken)
        {
            var set = _context.Instance.Set<TEntity>();
            set.AddRange(request.Entities);
            await _context.Instance.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
