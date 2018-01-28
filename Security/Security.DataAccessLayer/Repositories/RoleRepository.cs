using BusinessSolutions.Common.EntityFramework;
using Security.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity;

namespace Security.DataAccessLayer.Repositories
{
    public class RoleRepository : BaseEntityFrameworkRepository<Guid, Role>, IRoleRepository
    {
        public RoleRepository(SecurityContext dbContext) : base(dbContext)
        {
        }

        public Role FindByName(string roleName)
        {
            var role = Set.FirstOrDefault(c => c.Name == roleName);
            return role;
        }

        public async Task<Role> FindByNameAsync(string roleName)
        {
            var role = await Set.FirstOrDefaultAsync(c => c.Name == roleName);
            return role;
        }

        public async Task<Role> FindByNameAsync(CancellationToken cancellationToken, string roleName)
        {
            var role = await Set.FirstOrDefaultAsync(c => c.Name == roleName, cancellationToken);
            return role;
        }
        
    }
}
