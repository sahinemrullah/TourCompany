using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using TourCompany.Application.Common.Exceptions;
using TourCompany.Application.Common.Interfaces;

namespace TourCompany.Application.Generic.Commands.Update
{
    public class UpdateRangeRequest<T> : IRequest<Unit> where T : class
    {
        public UpdateRangeRequest(Expression<Func<T, object>> primaryKeySelector, params T[] dataTransferObjects)
        {
            DataTransferObjects = dataTransferObjects;
            PrimaryKeySelector = primaryKeySelector;
        }

        public IEnumerable<T> DataTransferObjects { get; set; }
        public Expression<Func<T, object>> PrimaryKeySelector { get; set; }
    }
    public class UpdateRangeCommand<TEntity, TDto> : IRequestHandler<UpdateRangeRequest<TDto>, Unit> where TEntity : class where TDto : class
    {
        private readonly ITourCompanyDbContext _context;

        public UpdateRangeCommand(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateRangeRequest<TDto> request, CancellationToken cancellationToken)
        {
            var pkSelector = request.PrimaryKeySelector.Compile();
            DbSet<TEntity> set = _context.Instance.Set<TEntity>();
            foreach (TDto dto in request.DataTransferObjects)
            {
                TEntity? entity = set.Find(pkSelector.Invoke(dto));
                if (entity == null)
                    throw new NotFoundException(typeof(TEntity).Name, pkSelector.Invoke(dto));
                EntityEntry<TEntity> entry = _context.Instance.Entry(entity);
                entry.CurrentValues.SetValues(dto);
            }
            await _context.Instance.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
    public class UpdateRangeCommand<TEntity> : IRequestHandler<UpdateRangeRequest<TEntity>, Unit> where TEntity : class
    {
        private readonly ITourCompanyDbContext _context;

        public UpdateRangeCommand(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateRangeRequest<TEntity> request, CancellationToken cancellationToken)
        {
            var pkSelector = request.PrimaryKeySelector.Compile();
            DbSet<TEntity> set = _context.Instance.Set<TEntity>();
            foreach (TEntity dto in request.DataTransferObjects)
            {
                TEntity? entity = set.Find(pkSelector.Invoke(dto));
                if (entity == null)
                    throw new NotFoundException(typeof(TEntity).Name, pkSelector.Invoke(dto));
                EntityEntry<TEntity> entry = _context.Instance.Entry(entity);
                entry.CurrentValues.SetValues(dto);
            }
            await _context.Instance.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
