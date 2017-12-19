using Security.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Security.DataAccessLayer.UnitOfWork
{
    public class SecurityUnitOfWork : ISecurityUnitOfWork
    {
        private SecurityContext _dbContext;
        public IRoleRepository RoleRepository { get; private set; }

        public IUserRepository UserRepository { get; private set; }

        public SecurityUnitOfWork() : this(new SecurityContext())
        {
           
        }

        public SecurityUnitOfWork(SecurityContext dataContext)
        {
            if (dataContext == null)
                throw new ArgumentNullException("dataContext");

            _dbContext = dataContext;
            UserRepository = new Repositories.UserRepository(dataContext);
            RoleRepository = new Repositories.RoleRepository(dataContext);
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

        ~SecurityUnitOfWork()
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
