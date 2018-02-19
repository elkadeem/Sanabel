using BusinessSolutions.Common.Core;
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

        public BaseUnitOfWork(DbContext dbContext)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(dbContext, nameof(dbContext));
            DbContext = dbContext;
        }

        public int Save()
        {
           return DbContext.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return DbContext.SaveChangesAsync();
        }

        public Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return DbContext.SaveChangesAsync(cancellationToken);
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
