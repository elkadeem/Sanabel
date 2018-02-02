using BusinessSolutions.Common.EntityFramework;
using BusinessSolutions.Common.Infra.Validation;
using Sanabel.Security.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Sanabel.Security.Infra
{
    public class RoleRepository : IRoleRepository
    {
        private readonly SecurityContext _dbContext;
        public RoleRepository(SecurityContext dbContext)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(dbContext, nameof(dbContext));
            _dbContext = dbContext;
        }

        public void Add(Role role)
        {
            _dbContext.Roles.Add(role);
        }

        public Role FindByName(string roleName)
        {
            return _dbContext.Roles.FirstOrDefault(c => c.Name == roleName);
        }

        public Task<Role> FindByNameAsync(string roleName)
        {
            return _dbContext.Roles.FirstOrDefaultAsync(c => c.Name == roleName);
        }

        public List<Role> GetAll()
        {
            return _dbContext.Roles.ToList();
        }

        public Task<Role> GetByIdAsync(Guid id)
        {
            return _dbContext.Roles.FirstOrDefaultAsync(c => c.Id == id);
        }

        public void Remove(Role role)
        {
            _dbContext.Roles.Remove(role);
        }

        public void Update(Role role)
        {
            var entry = _dbContext.Entry(role);
            if (entry.State == EntityState.Detached)
            {
                _dbContext.Roles.Attach(role);
                entry = _dbContext.Entry(role);
            }

            entry.State = EntityState.Modified;
        }
    }
}
