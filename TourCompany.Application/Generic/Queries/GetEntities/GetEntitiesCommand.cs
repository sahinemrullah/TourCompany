using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TourCompany.Application.Common.Interfaces;


namespace TourCompany.Application.Generic.Commands.GetEntities
{
    public class GetEntitiesRequest<TEntity, TDto> : IRequest<IEnumerable<TDto>> where TEntity : class where TDto : class
    {
        public GetEntitiesRequest(Expression<Func<TEntity, TDto>> mapFunc, Expression<Func<TEntity, bool>>? wherePredicate = null)
        {
            WherePredicate = wherePredicate;
            MapFunc = mapFunc;
        }

        public Expression<Func<TEntity, bool>>? WherePredicate { get; set; }
        public Expression<Func<TEntity, TDto>> MapFunc { get; internal set; }
    }
    public class GetEntitiesCommand<TEntity, TDto> : IRequestHandler<GetEntitiesRequest<TEntity, TDto>, IEnumerable<TDto>> where TEntity : class where TDto : class
    {
        private readonly ITourCompanyDbContext _context;

        public GetEntitiesCommand(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TDto>> Handle(GetEntitiesRequest<TEntity, TDto> request, CancellationToken cancellationToken)
        {
            DbSet<TEntity> set = _context.Instance.Set<TEntity>();
            IQueryable<TEntity> query = set;
            if (request.WherePredicate != null)
            {
                query = query.Where(request.WherePredicate);
            }

            return await query
                            .Select(request.MapFunc)
                            .ToListAsync(cancellationToken);
        }
    }

    public class GetEntitiesRequest<TEntity> : IRequest<IEnumerable<TEntity>> where TEntity : class
    {
        public GetEntitiesRequest(Expression<Func<TEntity, bool>>? wherePredicate = null)
        {
            WherePredicate = wherePredicate;
        }

        public Expression<Func<TEntity, bool>>? WherePredicate { get; set; }
    }
    public class GetEntitiesCommand<TEntity> : IRequestHandler<GetEntitiesRequest<TEntity>, IEnumerable<TEntity>> where TEntity : class
    {
        private readonly ITourCompanyDbContext _context;

        public GetEntitiesCommand(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> Handle(GetEntitiesRequest<TEntity> request, CancellationToken cancellationToken)
        {
            DbSet<TEntity> set = _context.Instance.Set<TEntity>();
            IQueryable<TEntity> query = set;
            if (request.WherePredicate != null)
            {
                query = query.Where(request.WherePredicate);
            }

            return await query.ToListAsync(cancellationToken);
        }
    }
}
