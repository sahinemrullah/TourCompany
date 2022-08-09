using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TourCompany.Application.Common.Interfaces;

namespace TourCompany.Application.Generic.Commands.GetEntity
{
    public class GetEntityRequest<TEntity, TDto> : IRequest<TDto?> where TEntity : class
    {
        public GetEntityRequest(Expression<Func<TEntity, TDto>> mapFunc, Expression<Func<TEntity, bool>>? wherePredicate = null, params string[] includeProperties)
        {
            WherePredicate = wherePredicate;
            IncludeProperties = includeProperties;
            MapFunc = mapFunc;
        }

        public Expression<Func<TEntity, bool>>? WherePredicate { get; set; }
        public IEnumerable<string> IncludeProperties { get; set; }
        public Expression<Func<TEntity, TDto>> MapFunc { get; set; }
    }
    public class GetEntityQuery<TEntity, TDto> : IRequestHandler<GetEntityRequest<TEntity, TDto>, TDto?> where TEntity : class
    {
        private readonly ITourCompanyDbContext _context;

        public GetEntityQuery(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<TDto?> Handle(GetEntityRequest<TEntity, TDto> request, CancellationToken cancellationToken)
        {
            DbSet<TEntity> set = _context.Instance.Set<TEntity>();
            IQueryable<TEntity> query = set;
            if (request.WherePredicate != null)
            {
                query = query.Where(request.WherePredicate);
            }
            if (request.IncludeProperties.Any())
            {
                foreach (var includeProperty in request.IncludeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.Select(request.MapFunc).FirstOrDefaultAsync(cancellationToken);
        }
    }

    public class GetEntityRequest<TEntity> : IRequest<TEntity?> where TEntity : class
    {
        public GetEntityRequest(Expression<Func<TEntity, bool>>? wherePredicate, params string[] includeProperties)
        {
            WherePredicate = wherePredicate;
            IncludeProperties = includeProperties;
        }

        public Expression<Func<TEntity, bool>>? WherePredicate { get; set; }
        public IEnumerable<string> IncludeProperties { get; set; }
    }
    public class GetEntityQuery<TEntity> : IRequestHandler<GetEntityRequest<TEntity>, TEntity?> where TEntity : class
    {
        private readonly ITourCompanyDbContext _context;

        public GetEntityQuery(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity?> Handle(GetEntityRequest<TEntity> request, CancellationToken cancellationToken)
        {
            DbSet<TEntity> set = _context.Instance.Set<TEntity>();
            IQueryable<TEntity> query = set;
            if (request.WherePredicate != null)
            {
                query = query.Where(request.WherePredicate);
            }
            if (request.IncludeProperties.Any())
            {
                foreach (var includeProperty in request.IncludeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
