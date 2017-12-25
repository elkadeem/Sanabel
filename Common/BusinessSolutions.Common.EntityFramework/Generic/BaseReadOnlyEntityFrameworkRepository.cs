using BusinessSolutions.Common.Core;
using BusinessSolutions.Common.Core.Entities;
using BusinessSolutions.Common.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.EntityFramework
{
    public abstract class BaseReadOnlyEntityFrameworkRepository<Tkey, TEntity> :
        IReadOnlyRepository<Tkey, TEntity>
        where TEntity : Entity<Tkey>
    {
        protected DbContext _dbContext;
        private DbSet<TEntity> _set;

        protected DbContext DbContext => _dbContext;

        protected DbSet<TEntity> Set => _set;

        public BaseReadOnlyEntityFrameworkRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _set = _dbContext.Set<TEntity>();
        }

        public virtual List<TEntity> GetAll()
        {
            return Set.ToList();
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            var result = await Set.ToListAsync();
            return result;
        }

        public virtual async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await Set.ToListAsync(cancellationToken);
            return result;
        }

        public virtual TEntity GetByID(Tkey key)
        {
            return Set.Find(key);
        }

        public virtual async Task<TEntity> GetByIDAsync(Tkey key)
        {
            var item = await Set.FindAsync(key);
            return item;
        }

        public virtual async Task<TEntity> GetByIDAsync(CancellationToken cancellationToken, Tkey key)
        {
            var item = await Set.FindAsync(cancellationToken, key);
            return item;
        }

        public virtual PagedEntity<TEntity> PageAll(int pageIndex, int pageSize)
        {
            var items = Set
                .Skip(pageIndex * pageSize)
                .Take(pageSize).ToList();

            int totalCount = Set.Count();
            return new PagedEntity<TEntity>(items, totalCount);
        }

        public virtual async Task<PagedEntity<TEntity>> PageAllAsync(int pageIndex, int pageSize)
        {
            var items = await Set
                .Skip(pageIndex * pageSize)
                .Take(pageSize).ToListAsync();

            int totalCount = Set.Count();
            return new PagedEntity<TEntity>(items, totalCount);
        }

        public virtual async Task<PagedEntity<TEntity>> PageAllAsync(CancellationToken cancellationToken, int pageIndex, int pageSize)
        {
            var items = await Set
                .Skip(pageIndex * pageSize)
                .Take(pageSize).ToListAsync(cancellationToken);

            int totalCount = Set.Count();
            return new PagedEntity<TEntity>(items, totalCount);
        }

        public List<TEntity> Find(ExpressionSpecification<TEntity> specification)
        {
            var query = Set.AsQueryable();
            if (specification != null)
                query = query.Where(specification.Expression);

            return query.ToList();
        }

        public PagedEntity<TEntity> Find(ExpressionSpecification<TEntity> specification, int pageIndex, int pageSize)
        {
            var query = Set.AsQueryable();
            if (specification != null)
                query = query.Where(specification.Expression);

            int totalItemsCount = query.Count();
            var items = query.OrderBy(c => c.Id)
                .Skip(pageIndex * pageSize)
                .Take(pageSize).ToList();

            return new PagedEntity<TEntity>(items, totalItemsCount);
        }
    }
}
