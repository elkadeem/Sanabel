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
    public class RoleRepository : BaseEntityFrameworkRepository<Guid, Role, Domain.Role>, IRoleRepository
    {
        public RoleRepository(SecurityContext dbContext) : base(dbContext)
        {
        }

        public Domain.Role FindByName(string roleName)
        {
            var role = Set.FirstOrDefault(c => c.RoleName == roleName);
            return GetDomainEntity(role);
        }

        public async Task<Domain.Role> FindByNameAsync(string roleName)
        {
            var role = await Set.FirstOrDefaultAsync(c => c.RoleName == roleName);
            return GetDomainEntity(role);
        }

        public async Task<Domain.Role> FindByNameAsync(CancellationToken cancellationToken, string roleName)
        {
            var role = await Set.FirstOrDefaultAsync(c => c.RoleName == roleName, cancellationToken);
            return GetDomainEntity(role);
        }

        public override Domain.Role GetDomainEntity(Role entity)
        {
            if (entity == null)
                return null;
            return new Domain.Role
            {
                RoleId = entity.RoleId,
                RoleName = entity.RoleName,
                RoleNameAr = entity.RoleNameAr
            };
        }

        public override Role GetEntity(Domain.Role domainEntity)
        {
            if (domainEntity == null)
                return null;
            return new Role
            {
                RoleId = domainEntity.RoleId,
                RoleName = domainEntity.RoleName,
                RoleNameAr = domainEntity.RoleNameAr
            };
        }
    }
}
