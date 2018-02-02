using BusinessSolutions.Common.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sanabel.Security.Domain
{
    public interface IRoleRepository
    {
        Task<Role> FindByNameAsync(string roleName);
        List<Role> GetAll();
        void Add(Role role);
        Task<Role> GetByIdAsync(Guid id);
        void Remove(Role role);
        void Update(Role role);
        Role FindByName(string roleName);
    }
}
