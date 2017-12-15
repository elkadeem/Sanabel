using BusinessSolutions.Common.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Security.Domain
{
    public interface IRoleRepository : IRepository<Guid, Role>
    {
        Role FindByName(string roleName);

        Task<Role> FindByNameAsync(string roleName);

        Task<Role> FindByNameAsync(CancellationToken cancellationToken, string roleName);
    }
}
