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
        private SecurityContext _dbContext;
        public IRoleRepository RoleRepository { get; private set; }

        public IUserRepository UserRepository { get; private set; }
        
        public SecurityUnitOfWork() : this(new SecurityContext())
        {

        }

        public SecurityUnitOfWork(SecurityContext dbContext) : base(dbContext)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(dbContext, nameof(dbContext));
            _dbContext = dbContext;
            UserRepository = new UserRepository(dbContext);
            RoleRepository = new RoleRepository(dbContext);            
        }
    }
}
