using System;
using System.Threading;
using System.Threading.Tasks;
using BusinessSolutions.Common.Core;
using Sanable.Cases.Domain.Repositories;

namespace Sanable.Cases.Infra
{
    public class CaseUnitOfWork : ICaseUnitOfWork
    {
        private CaseResearchDataContext _dbContext;

        public ICaseRepository CaseRepository { get; private set; }

        public ICaseResearchRepository CaseResearchRepository { get; private set; }
        
        public CaseUnitOfWork(CaseResearchDataContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");

            _dbContext = dbContext;
            CaseRepository = new CaseRepository(_dbContext);
            CaseResearchRepository = new CaseResearchRepository(_dbContext);
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

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

        ~CaseUnitOfWork()
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
