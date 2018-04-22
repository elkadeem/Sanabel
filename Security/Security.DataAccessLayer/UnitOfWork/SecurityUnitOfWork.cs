using BusinessSolutions.Common.EntityFramework;
using BusinessSolutions.Common.Infra.Validation;
using Sanabel.Security.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sanabel.Security.Infra
{
    public class SecurityUnitOfWork : BaseUnitOfWork, ISecurityUnitOfWork
    {
        private readonly SecurityContext _dbContext;
        public IRoleRepository RoleRepository { get; private set; }

        public IUserRepository UserRepository { get; private set; }
        
        public SecurityUnitOfWork(SecurityContext dbContext) : base(dbContext)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(dbContext, nameof(dbContext));
            _dbContext = dbContext;
            UserRepository = new UserRepository(_dbContext);
            RoleRepository = new RoleRepository(_dbContext);            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (RoleRepository != null)
                    RoleRepository = null;
                if (UserRepository != null)
                    UserRepository = null;
            }
            base.Dispose(disposing);
        }
    }
}
