using Microsoft.AspNet.Identity;
using Security.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.AspIdentity
{
    public class RoleStore : IRoleStore<Role, Guid>, IQueryableRoleStore<Role, Guid>
    {
        private ISecurityUnitOfWork _securityUnitOfWork;

        public RoleStore(ISecurityUnitOfWork securityUnitOfWork)
        {
            _securityUnitOfWork = securityUnitOfWork;
        }

        public IQueryable<Role> Roles
        {
            get
            {
                return _securityUnitOfWork.RoleRepository.GetAll()                    
                    .AsQueryable();
            }
        }

        public Task CreateAsync(Role role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            _securityUnitOfWork.RoleRepository.Add(role);
            return _securityUnitOfWork.SaveAsync();
        }

        public Task DeleteAsync(Role role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            var roleEntity = _securityUnitOfWork.RoleRepository.GetByID(role.Id);
            if (roleEntity == null)
                throw new ArgumentException("Role is not exist.", "role");

            _securityUnitOfWork.RoleRepository.Remove(roleEntity.Id);
            return _securityUnitOfWork.SaveAsync();
        }

        public Task<Role> FindByIdAsync(Guid Id)
        {
            var roleEntity = _securityUnitOfWork.RoleRepository.GetByID(Id);
            return Task.FromResult(roleEntity);
        }

        public Task<Role> FindByNameAsync(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
                throw new ArgumentNullException("roleName");

            var roleEntity = _securityUnitOfWork.RoleRepository.FindByName(roleName);
            return Task.FromResult(roleEntity);
        }

        public Task UpdateAsync(Role role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            var roleEntity = _securityUnitOfWork.RoleRepository.GetByID(role.Id);
            if (roleEntity == null)
                throw new ArgumentException("Role is not exist.", "role");

            _securityUnitOfWork.RoleRepository.Update(role);
            return _securityUnitOfWork.SaveAsync();
        }

        

        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~RoleStore()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_securityUnitOfWork != null)
                {
                    _securityUnitOfWork.Dispose();
                }
            }
        }
        #endregion 
    }
}
