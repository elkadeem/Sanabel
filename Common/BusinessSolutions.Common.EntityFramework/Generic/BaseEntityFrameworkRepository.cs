using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity;
using BusinessSolutions.Common.Core;
using System.Data.Entity.Infrastructure;

namespace BusinessSolutions.Common.EntityFramework
{
    public abstract class BaseEntityFrameworkRepository<Tkey, TEntity, TDomainEntity>
        : BaseReadOnlyEntityFrameworkRepository<Tkey, TEntity, TDomainEntity>
        , IRepository<Tkey, TDomainEntity>
        where TDomainEntity : class where TEntity : class
    {
        private Dictionary<TDomainEntity, TEntity> _updatedEntities;

        public BaseEntityFrameworkRepository(DbContext dbContext) : base(dbContext)
        {
            _updatedEntities = new Dictionary<TDomainEntity, TEntity>();
        }

        public virtual void Add(TDomainEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            TEntity item = GetEntity(entity);
            Set.Add(item);
            AddTrackedEntity(entity, item);
        }

        public virtual void Remove(Tkey id)
        {
            var item = Set.Find(id);
            if (item != null)
                Set.Remove(item);
        }

        public virtual void Update(TDomainEntity entity)
        {
            var item = GetEntity(entity);
            var entry = _dbContext.Entry<TEntity>(item);
            if (entry.State == EntityState.Detached)
            {

                Set.Attach(item);
                entry = _dbContext.Entry(item);
            }

            entry.State = EntityState.Modified;
            AddTrackedEntity(entity, item);
        }

        private void AddTrackedEntity(TDomainEntity entity, TEntity item)
        {
            if (_updatedEntities.ContainsKey(entity))
                _updatedEntities[entity] = item;
            else
                _updatedEntities.Add(entity, item);
        }

        public virtual Tkey GetPrimaryKey(TDomainEntity entity)
        {
            if (entity == null || !_updatedEntities.ContainsKey(entity))
                return default(Tkey);

            TEntity item = _updatedEntities[entity];
            if (item == null)
                return default(Tkey);

            var dbEntry = _dbContext.ChangeTracker.Entries<TEntity>().FirstOrDefault(c => c.Entity == item);
            var objectStateEntry = ((IObjectContextAdapter)_dbContext).ObjectContext.ObjectStateManager
                .GetObjectStateEntry(dbEntry.Entity);

            return (Tkey)objectStateEntry.EntityKey.EntityKeyValues[0].Value;

        }

    }
}
