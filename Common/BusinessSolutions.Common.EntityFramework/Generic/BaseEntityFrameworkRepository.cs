using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity;
using BusinessSolutions.Common.Core;
using System.Data.Entity.Infrastructure;
using BusinessSolutions.Common.Core.Entities;

namespace BusinessSolutions.Common.EntityFramework
{
    public class BaseEntityFrameworkRepository<Tkey, TEntity>
        : BaseReadOnlyEntityFrameworkRepository<Tkey, TEntity>
        , IRepository<Tkey, TEntity>
        where TEntity : Entity<Tkey>
    {
        public BaseEntityFrameworkRepository(DbContext dbContext) : base(dbContext)
        {

        }

        public virtual void Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Set.Add(entity);
        }

        public virtual void Remove(Tkey id)
        {
            var item = Set.Find(id);
            if (item != null)
                Set.Remove(item);
        }

        public virtual void Remove(TEntity entity)
        {
            if (entity != null)
                Set.Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            var item = Set.Local.FirstOrDefault(c => entity.Equals(c));
            if (item != null
                && _dbContext.Entry(item).State != EntityState.Detached)
            {
                _dbContext.Entry(item).CurrentValues.SetValues(entity);
            }
            else
            {
                var entry = _dbContext.Entry<TEntity>(entity);
                if (entry.State == EntityState.Detached)
                {

                    Set.Attach(entity);
                    entry = _dbContext.Entry(entity);
                }

                entry.State = EntityState.Modified;
            }
        }

        public virtual Tkey GetPrimaryKey(TEntity entity)
        {
            if (entity == null)
                return default(Tkey);

            var dbEntry = _dbContext.ChangeTracker.Entries<TEntity>().FirstOrDefault(c => c.Entity == entity);
            var objectStateEntry = ((IObjectContextAdapter)_dbContext).ObjectContext.ObjectStateManager
                .GetObjectStateEntry(dbEntry.Entity);

            return (Tkey)objectStateEntry.EntityKey.EntityKeyValues[0].Value;

        }

    }
}
