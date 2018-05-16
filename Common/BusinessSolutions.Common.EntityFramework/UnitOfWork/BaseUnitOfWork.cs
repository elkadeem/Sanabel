using BusinessSolutions.Common.Core;
using BusinessSolutions.Common.Core.Entities;
using BusinessSolutions.Common.Core.Events;
using BusinessSolutions.Common.Infra.Validation;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.EntityFramework
{
    public class BaseUnitOfWork : IUnitOfWork
    {
        protected DbContext DbContext;
        protected DbContextTransaction _dbContextTransaction;

        public void BeginTransaction()
        {
            _dbContextTransaction = DbContext.Database.BeginTransaction();
        }

        public BaseUnitOfWork(DbContext dbContext)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(dbContext, nameof(dbContext));
            DbContext = dbContext;

        }

        public int Save()
        {
            List<AggregateRoot> aggregateRoots = GetAggregateRoles();
            int result = DbContext.SaveChanges();
            FireDomainEvents(aggregateRoots);
            return result;
        }

        private List<AggregateRoot> GetAggregateRoles()
        {
            var items = DbContext.ChangeTracker.Entries();
            var aggregateRoots = items.Where(c => c.Entity is AggregateRoot)
                .Select(c => c.Entity as AggregateRoot).ToList();
            return aggregateRoots;
        }

        public Task<int> SaveAsync()
        {
            List<AggregateRoot> aggregateRoots = GetAggregateRoles();
            return DbContext.SaveChangesAsync().ContinueWith((saveTask) =>
            {
                FireDomainEvents(aggregateRoots);
                return saveTask.Result;
            });
        }

        public Task<int> ApproveAsync()
        {
            List<AggregateRoot> aggregateRoots = GetAggregateRoles();
            return DbContext.SaveChangesAsync().ContinueWith((saveTask) =>
            {
                FireDomainEvents(aggregateRoots);
                return saveTask.Result;
            });
        }

        public Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            List<AggregateRoot> aggregateRoots = GetAggregateRoles();
            return DbContext.SaveChangesAsync(cancellationToken)
                .ContinueWith((saveTask) =>
                {
                    FireDomainEvents(aggregateRoots);
                    return saveTask.Result;
                });
        }

        public void Commit()
        {
            if (_dbContextTransaction != null)
                _dbContextTransaction.Commit();
        }

        public void Rollback()
        {
            if (_dbContextTransaction != null)
                _dbContextTransaction.Rollback();
        }

        private void FireDomainEvents(IEnumerable<AggregateRoot> items)
        {
            foreach (var item in items)
            {
                foreach (IDomainEvent domainEvent in item.DomainEvents)
                {
                    DomainEvents.Dispatch(domainEvent);
                }
                item.ClearDomainEvents();
            }
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (DbContext != null)
                    {
                        DbContext.Dispose();
                    }
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
