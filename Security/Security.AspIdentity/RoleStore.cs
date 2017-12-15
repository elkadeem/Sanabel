using Microsoft.AspNet.Identity;
using Security.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.AspIdentity
{
    public class RoleStore : IRoleStore<ApplicationRole, Guid>, IQueryableRoleStore<ApplicationRole, Guid>
    {
        private ISecurityUnitOfWork _securityUnitOfWork;

        public RoleStore(ISecurityUnitOfWork securityUnitOfWork)
        {
            _securityUnitOfWork = securityUnitOfWork;
        }

        public IQueryable<ApplicationRole> Roles
        {
            get
            {
                return _securityUnitOfWork.RoleRepository.GetAll()
                    .Select(c => GetApplicationRole(c))
                    .AsQueryable();
            }
        }

        public Task CreateAsync(ApplicationRole role)
        {
            if (role == null)
                throw new ArgumentNullException("role");
            
            _securityUnitOfWork.RoleRepository.Add(role);
            return _securityUnitOfWork.SaveAsync();
        }

        public Task DeleteAsync(ApplicationRole role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            var roleEntity = _securityUnitOfWork.RoleRepository.GetByID(role.Id);
            if (roleEntity == null)
                throw new ArgumentException("Role is not exist.", "role");

            _securityUnitOfWork.RoleRepository.Remove(roleEntity.RoleId);
            return _securityUnitOfWork.SaveAsync();
        }

        public Task<ApplicationRole> FindByIdAsync(Guid roleId)
        {
            var roleEntity = _securityUnitOfWork.RoleRepository.GetByID(roleId);
            return Task.FromResult(GetApplicationRole(roleEntity));
        }

        public Task<ApplicationRole> FindByNameAsync(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
                throw new ArgumentNullException("roleName");

            var roleEntity = _securityUnitOfWork.RoleRepository.FindByName(roleName);
            return Task.FromResult(GetApplicationRole(roleEntity));
        }

        public Task UpdateAsync(ApplicationRole role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            var roleEntity = _securityUnitOfWork.RoleRepository.GetByID(role.Id);
            if (roleEntity == null)
                throw new ArgumentException("Role is not exist.", "role");

            _securityUnitOfWork.RoleRepository.Update(role);
            return _securityUnitOfWork.SaveAsync();
        }

        #region private methods
        private ApplicationRole GetApplicationRole(Role role)
        {
            if (role == null)
                return null;
            return new ApplicationRole
            {
                RoleId = role.RoleId,
                Name = role.RoleName,
            };
        }
        #endregion

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
