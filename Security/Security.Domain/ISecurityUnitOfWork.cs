using BusinessSolutions.Common.Core;

namespace Sanabel.Security.Domain
{
    public interface ISecurityUnitOfWork : IUnitOfWork
    {
        IRoleRepository RoleRepository { get; }

        IUserRepository UserRepository { get; }        
    }
}
