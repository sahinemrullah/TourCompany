using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TourCompany.Application.Common.DataTransferObjects;
using TourCompany.Application.Common.Interfaces;


namespace TourCompany.Application.Generic.Commands.GetEntities
{
    public class GetPaginatedEntitiesRequest<TEntity, TDto> : IRequest<PaginatedList<TDto>> where TEntity : class where TDto : class
    {
        public GetPaginatedEntitiesRequest(Expression<Func<TEntity, TDto>> mapFunc, int pageNumber = 1, int pageSize = 10, Expression<Func<TEntity, bool>>? wherePredicate = null)
        {
            WherePredicate = wherePredicate;
            MapFunc = mapFunc;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public Expression<Func<TEntity, bool>>? WherePredicate { get; set; }
        public Expression<Func<TEntity, TDto>> MapFunc { get; internal set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetPaginatedEntitiesCommand<TEntity, TDto> : IRequestHandler<GetPaginatedEntitiesRequest<TEntity, TDto>, PaginatedList<TDto>> where TEntity : class where TDto : class
    {
        private readonly ITourCompanyDbContext _context;

        public GetPaginatedEntitiesCommand(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<TDto>> Handle(GetPaginatedEntitiesRequest<TEntity, TDto> request, CancellationToken cancellationToken)
        {
            DbSet<TEntity> set = _context.Instance.Set<TEntity>();
            IQueryable<TEntity> query = set;
            if (request.WherePredicate != null)
            {
                query = query.Where(request.WherePredicate);
            }

            return new PaginatedList<TDto>()
            {
                Items = await query
                            .Skip((request.PageNumber - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .Select(request.MapFunc)
                            .ToListAsync(cancellationToken),
                Count = await set.CountAsync(cancellationToken),
            };
        }
    }

    public class GetPaginatedEntitiesRequest<TEntity> : IRequest<PaginatedList<TEntity>> where TEntity : class
    {
        public GetPaginatedEntitiesRequest(Expression<Func<TEntity, bool>>? wherePredicate = null)
        {
            WherePredicate = wherePredicate;
        }

        public Expression<Func<TEntity, bool>>? WherePredicate { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetPaginatedEntitiesCommand<TEntity> : IRequestHandler<GetPaginatedEntitiesRequest<TEntity>, PaginatedList<TEntity>> where TEntity : class
    {
        private readonly ITourCompanyDbContext _context;

        public GetPaginatedEntitiesCommand(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<TEntity>> Handle(GetPaginatedEntitiesRequest<TEntity> request, CancellationToken cancellationToken)
        {
            DbSet<TEntity> set = _context.Instance.Set<TEntity>();
            IQueryable<TEntity> query = set;
            if (request.WherePredicate != null)
            {
                query = query.Where(request.WherePredicate);
            }

            return new PaginatedList<TEntity>()
            {
                Items = await query
                            .Skip((request.PageNumber - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .ToListAsync(cancellationToken),
                Count = await set.CountAsync(cancellationToken),
            };
        }
    }
}
