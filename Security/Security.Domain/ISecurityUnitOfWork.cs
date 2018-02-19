using BusinessSolutions.Common.Core;
using System;

namespace Sanabel.Security.Domain
{
    public interface ISecurityUnitOfWork : IUnitOfWork
    {
        IRoleRepository RoleRepository { get; }

        IUserRepository UserRepository { get; }        
    }
}
