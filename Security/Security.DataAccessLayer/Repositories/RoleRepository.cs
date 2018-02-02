using BusinessSolutions.Common.EntityFramework;
using Sanabel.Security.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sanabel.Security.Infra
{
    public class RoleRepository : IRoleRepository
    {
        BaseEntityFrameworkRepository<Guid, Role> _repository;
        public RoleRepository(SecurityContext dbContext)
        {
            _repository = new BaseEntityFrameworkRepository<Guid, Role>(dbContext);
        }

        public void Add(Role role)
        {
            throw new NotImplementedException();
        }

        public Role FindByName(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<Role> FindByNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public List<Role> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Role> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Remove(object roleEntity)
        {
            throw new NotImplementedException();
        }

        public void Update(Role role)
        {
            throw new NotImplementedException();
        }
    }
}
