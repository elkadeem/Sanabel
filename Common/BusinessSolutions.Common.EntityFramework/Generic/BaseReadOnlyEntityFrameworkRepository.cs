using BusinessSolutions.Common.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.EntityFramework
{
    public abstract class BaseReadOnlyEntityFrameworkRepository<Tkey, TEntity, TDomainEntity> : IDisposable
        , IReadOnlyRepository<Tkey, TDomainEntity>
        where TDomainEntity : class where TEntity : class
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

        public virtual List<TDomainEntity> GetAll()
        {
            return Set.Select(c => GetDomainEntity(c)).ToList();
        }

        public virtual async Task<List<TDomainEntity>> GetAllAsync()
        {
            var result = await Set.ToListAsync();
            return result.Select(c => GetDomainEntity(c)).ToList();
        }

        public virtual async Task<List<TDomainEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await Set.ToListAsync(cancellationToken);
            return result.Select(c => GetDomainEntity(c)).ToList();
        }

        public virtual TDomainEntity GetByID(Tkey key)
        {
            var item = Set.Find(key);
            if (item == null)
                return null;

            return GetDomainEntity(item);
        }

        public virtual async Task<TDomainEntity> GetByIDAsync(Tkey key)
        {
            var item = await Set.FindAsync(key);
            if (item == null)
                return null;

            return GetDomainEntity(item);
        }

        public virtual async Task<TDomainEntity> GetByIDAsync(CancellationToken cancellationToken, Tkey key)
        {
            var item = await Set.FindAsync(cancellationToken, key);
            if (item == null)
                return null;

            return GetDomainEntity(item);
        }

        public virtual PagedEntity<TDomainEntity> PageAll(int pageIndex, int pageSize)
        {
            var items = Set
                .Skip(pageIndex * pageSize)
                .Take(pageSize).Select(c => GetDomainEntity(c)).ToList();

            int totalCount = Set.Count();
            return new PagedEntity<TDomainEntity>(items, totalCount);
        }

        public virtual async Task<PagedEntity<TDomainEntity>> PageAllAsync(int pageIndex, int pageSize)
        {
            var items = await Set
                .Skip(pageIndex * pageSize)
                .Take(pageSize).Select(c => GetDomainEntity(c)).ToListAsync();

            int totalCount = Set.Count();
            return new PagedEntity<TDomainEntity>(items, totalCount);
        }

        public virtual async Task<PagedEntity<TDomainEntity>> PageAllAsync(CancellationToken cancellationToken, int pageIndex, int pageSize)
        {
            var items = await Set
                .Skip(pageIndex * pageSize)
                .Take(pageSize).Select(c => GetDomainEntity(c)).ToListAsync(cancellationToken);

            int totalCount = Set.Count();
            return new PagedEntity<TDomainEntity>(items, totalCount);
        }
        
        public abstract TEntity GetEntity(TDomainEntity domainEntity);

        public abstract TDomainEntity GetDomainEntity(TEntity entity);

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_dbContext != null)
                        _dbContext.Dispose();
                }
                disposedValue = true;
            }
        }

        ~BaseReadOnlyEntityFrameworkRepository()
        {
            Dispose(false);
        }
       
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
