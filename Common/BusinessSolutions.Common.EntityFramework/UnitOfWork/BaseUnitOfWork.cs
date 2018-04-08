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
            var items = DbContext.ChangeTracker.Entries();
            int result = DbContext.SaveChanges();
            FireDomainEvents(items);
            return result;
        }

        public Task<int> SaveAsync()
        {
            var items = DbContext.ChangeTracker.Entries();
            return DbContext.SaveChangesAsync().ContinueWith((saveTask) =>
            {
                FireDomainEvents(items);
                return saveTask.Result;
            });
        }

        public Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            var items = DbContext.ChangeTracker.Entries();
            return DbContext.SaveChangesAsync(cancellationToken)
                .ContinueWith((saveTask) =>
                {
                    FireDomainEvents(items);
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

        private void FireDomainEvents(IEnumerable<System.Data.Entity.Infrastructure.DbEntityEntry> items)
        {
            foreach (var item in items.Where(c => c.Entity is AggregateRoot))
            {
                AggregateRoot aggregateRoot = item.Entity as AggregateRoot;
                foreach (IDomainEvent domainEvent in aggregateRoot.DomainEvents)
                {
                    DomainEvents.Dispatch(domainEvent);
                }

                aggregateRoot.ClearDomainEvents();
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
