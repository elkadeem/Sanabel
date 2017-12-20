using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity;
using BusinessSolutions.Common.Core;

namespace BusinessSolutions.Common.EntityFramework
{
    public abstract class BaseEntityFrameworkRepository<Tkey, TEntity, TDomainEntity> 
        : BaseReadOnlyEntityFrameworkRepository<Tkey, TEntity, TDomainEntity>
        , IRepository<Tkey, TDomainEntity>        
        where TDomainEntity : class where TEntity : class
    {
        public BaseEntityFrameworkRepository(DbContext dbContext) : base(dbContext)
        {            
        }

        public virtual void Add(TDomainEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            TEntity item = GetEntity(entity);
            Set.Add(item);
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
        }

    }
}
