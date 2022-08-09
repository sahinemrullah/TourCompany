using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using TourCompany.Application.Common.Exceptions;
using TourCompany.Application.Common.Interfaces;

namespace TourCompany.Application.Generic.Commands.Update
{
    public class UpdateRequest<T> : IRequest<Unit> where T : class
    {
        public UpdateRequest(Expression<Func<T, object>> primaryKeySelector, T updatedEntity)
        {
            UpdatedEntity = updatedEntity;
            PrimaryKeySelector = primaryKeySelector;
        }

        public T UpdatedEntity { get; set; }
        public Expression<Func<T, object>> PrimaryKeySelector { get; set; }
    }
    public class UpdateCommand<TEntity, TDto> : IRequestHandler<UpdateRequest<TDto>, Unit> where TEntity : class where TDto : class
    {
        private readonly ITourCompanyDbContext _context;

        public UpdateCommand(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateRequest<TDto> request, CancellationToken cancellationToken)
        {
            var pkSelector = request.PrimaryKeySelector.Compile();
            DbSet<TEntity> set = _context.Instance.Set<TEntity>();
            TEntity? entity = set.Find(pkSelector.Invoke(request.UpdatedEntity));
            if (entity == null)
                throw new NotFoundException(typeof(TEntity).Name, pkSelector.Invoke(request.UpdatedEntity));
            EntityEntry<TEntity> entry = _context.Instance.Entry(entity);
            entry.CurrentValues.SetValues(request.UpdatedEntity);
            await _context.Instance.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
    public class UpdateCommand<TEntity> : IRequestHandler<UpdateRequest<TEntity>, Unit> where TEntity : class
    {
        private readonly ITourCompanyDbContext _context;

        public UpdateCommand(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateRequest<TEntity> request, CancellationToken cancellationToken)
        {
            var pkSelector = request.PrimaryKeySelector.Compile();
            DbSet<TEntity> set = _context.Instance.Set<TEntity>();
            TEntity? entity = set.Find(pkSelector.Invoke(request.UpdatedEntity));
            if (entity == null)
                throw new NotFoundException(typeof(TEntity).Name, pkSelector.Invoke(request.UpdatedEntity));
            EntityEntry<TEntity> entry = _context.Instance.Entry(entity);
            entry.CurrentValues.SetValues(request.UpdatedEntity);
            await _context.Instance.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
