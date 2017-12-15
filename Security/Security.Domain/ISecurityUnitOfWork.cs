using BusinessSolutions.Common.Core;

namespace Security.Domain
{
    public interface ISecurityUnitOfWork : IUnitOfWork
    {
        IRoleRepository RoleRepository { get; }

        IUserRepository UserRepository { get; }
    }
}
