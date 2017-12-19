using Microsoft.AspNet.Identity;
using Security.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.AspIdentity
{
    public class UserStore : IUserStore<ApplicationUser, Guid>, IUserLoginStore<ApplicationUser, Guid>
        , IUserClaimStore<ApplicationUser, Guid>, IUserEmailStore<ApplicationUser, Guid>
        , IUserLockoutStore<ApplicationUser, Guid>, IUserPasswordStore<ApplicationUser, Guid>
        , IUserPhoneNumberStore<ApplicationUser, Guid>, IUserSecurityStampStore<ApplicationUser, Guid>
        , IUserTwoFactorStore<ApplicationUser, Guid>, IUserRoleStore<ApplicationUser, Guid>
    {
        private ISecurityUnitOfWork _securityUnitOfWork;

        public UserStore(ISecurityUnitOfWork securityUnitOfWork)
        {
            _securityUnitOfWork = securityUnitOfWork;
        }

        #region IUserStore
        public Task CreateAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            _securityUnitOfWork.UserRepository.Add(user);
            return _securityUnitOfWork.SaveAsync();
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            _securityUnitOfWork.UserRepository.Remove(user.Id);
            return _securityUnitOfWork.SaveAsync();
        }

        public Task<ApplicationUser> FindByIdAsync(Guid userId)
        {
            var user = _securityUnitOfWork.UserRepository.GetByID(userId);
            var applicationUser = GetApplicationUser(user);
            return Task.FromResult(applicationUser);
        }

        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException("userName");

            var user = await _securityUnitOfWork.UserRepository.FindByUserNameAsync(userName);
            var applicationUser = GetApplicationUser(user);
            return applicationUser;
        }

        public Task UpdateAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var entity = _securityUnitOfWork.UserRepository.GetByIDAsync(user.Id);
            if (entity == null)
                throw new ArgumentException("User is not found.", "user");

            _securityUnitOfWork.UserRepository.Update(user);
            return _securityUnitOfWork.SaveAsync();
        }
        #endregion

        #region IUserLoginStore
        public Task AddLoginAsync(ApplicationUser user, UserLoginInfo login)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (login == null)
                throw new ArgumentNullException("login");

            var userEntity = _securityUnitOfWork.UserRepository.GetByID(user.Id);
            if (userEntity == null)
                throw new ArgumentException("User is not exist.", "user");

            userEntity.AddExternalLogin(login.LoginProvider, login.ProviderKey);
            _securityUnitOfWork.UserRepository.Update(userEntity);
            return _securityUnitOfWork.SaveAsync();
        }

        public async Task<ApplicationUser> FindAsync(UserLoginInfo login)
        {
            if (login == null)
                throw new ArgumentNullException("login");

            var user = await _securityUnitOfWork.UserRepository.FindByLoginAsync(login.LoginProvider, login.ProviderKey);
            return GetApplicationUser(user);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var entityUser = _securityUnitOfWork.UserRepository.GetByID(user.Id);
            if (entityUser == null)
                throw new ArgumentException("User is not found.", "user");

            IList<UserLoginInfo> result = null;
            if (entityUser.ExternalLogins != null || entityUser.ExternalLogins.Count > 0)
                result = entityUser.ExternalLogins.Select(c => new UserLoginInfo(c.LoginProvider, c.ProviderKey))
                    .ToList();

            return Task.FromResult(result);
        }

        public Task RemoveLoginAsync(ApplicationUser user, UserLoginInfo login)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (login == null)
                throw new ArgumentNullException("login");

            var userEntity = _securityUnitOfWork.UserRepository.GetByID(user.Id);
            if (userEntity == null)
                throw new ArgumentException("User is not found", "user");

            userEntity.RemoveExternalLogin(login.LoginProvider);
            _securityUnitOfWork.UserRepository.Update(user);
            return _securityUnitOfWork.SaveAsync();

        }
        #endregion 

        #region IUserClaimStore
        public async Task<IList<System.Security.Claims.Claim>> GetClaimsAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var entityUser = await _securityUnitOfWork.UserRepository.GetByIDAsync(user.Id);

            if (entityUser == null)
                throw new ArgumentException("User is not found.", "user");

            IList<System.Security.Claims.Claim> claims = entityUser.Claims.Select(c => new System.Security.Claims.Claim(c.ClaimType, c.ClaimValue)).ToList();
            return claims;
        }

        public Task AddClaimAsync(ApplicationUser user, System.Security.Claims.Claim claim)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (claim == null)
                throw new ArgumentNullException("claim");

            var entityUser = _securityUnitOfWork.UserRepository.GetByID(user.Id);
            if (entityUser == null)
                throw new ArgumentException("User is not found.", "user");

            entityUser.AddClaim(claim.Type, claim.Value);
            _securityUnitOfWork.UserRepository.Update(entityUser);
            return _securityUnitOfWork.SaveAsync();
        }

        public Task RemoveClaimAsync(ApplicationUser user, System.Security.Claims.Claim claim)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (claim == null)
                throw new ArgumentNullException("claim");

            var entityUser = _securityUnitOfWork.UserRepository.GetByID(user.Id);

            if (entityUser == null)
                throw new ArgumentException("User is not found.", "user");

            entityUser.RemoveClaim(claim.Type, claim.Value);

            _securityUnitOfWork.UserRepository.Update(entityUser);
            return _securityUnitOfWork.SaveAsync();
        }
        #endregion

        #region IUserEmailStore
        public Task SetEmailAsync(ApplicationUser user, string email)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("email");

            var entity = _securityUnitOfWork.UserRepository.GetByID(user.Id);
            if (entity == null)
                throw new ArgumentException("User is not found.", "user");

            entity.Email = email;
            entity.IsEmailConfirmed = false;

            _securityUnitOfWork.UserRepository.Update(entity);
            return _securityUnitOfWork.SaveAsync();
        }

        public Task<string> GetEmailAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var entity = _securityUnitOfWork.UserRepository.GetByID(user.Id);
            if (entity == null)
                throw new ArgumentException("User is not found.", "user");

            return Task.FromResult(entity.IsEmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var entity = _securityUnitOfWork.UserRepository.GetByID(user.Id);
            if (entity == null)
                throw new ArgumentException("User is not found.", "user");

            entity.IsEmailConfirmed = confirmed;
            _securityUnitOfWork.UserRepository.Update(entity);
            return _securityUnitOfWork.SaveAsync();
        }

        public Task<ApplicationUser> FindByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("email");

            var entity = _securityUnitOfWork.UserRepository.FindByEmail(email);
            var user = GetApplicationUser(entity);
            return Task.FromResult(user);
        }
        #endregion

        #region IUserLockoutStore
        public Task<DateTimeOffset> GetLockoutEndDateAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var entity = _securityUnitOfWork.UserRepository.GetByID(user.Id);
            if (entity == null)
                throw new ArgumentException("User is not found.", "user");

            DateTimeOffset date = DateTimeOffset.MinValue;

            if (entity.LockedOutDate.HasValue)
                date = new DateTimeOffset(entity.LockedOutDate.Value);

            return Task.FromResult(date);
        }

        public Task SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset lockoutEnd)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var entity = _securityUnitOfWork.UserRepository.GetByID(user.Id);
            if (entity == null)
                throw new ArgumentException("User is not found.", "user");

            if (lockoutEnd == DateTimeOffset.MinValue)
                entity.LockedOutDate = null;
            else
                entity.LockedOutDate = lockoutEnd.UtcDateTime;

            _securityUnitOfWork.UserRepository.Update(entity);
            return _securityUnitOfWork.SaveAsync();
        }

        public Task<int> IncrementAccessFailedCountAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var entity = _securityUnitOfWork.UserRepository.GetByID(user.Id);
            if (entity == null)
                throw new ArgumentException("User is not found.", "user");

            entity.AccessFailedCount = entity.AccessFailedCount + 1;
            _securityUnitOfWork.UserRepository.Update(entity);
            _securityUnitOfWork.Save();

            return Task.FromResult(entity.AccessFailedCount);
        }

        public Task ResetAccessFailedCountAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var entity = _securityUnitOfWork.UserRepository.GetByID(user.Id);
            if (entity == null)
                throw new ArgumentException("User is not found.", "user");

            entity.AccessFailedCount = 0;
            _securityUnitOfWork.UserRepository.Update(entity);
            return _securityUnitOfWork.SaveAsync();
        }

        public Task<int> GetAccessFailedCountAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var entity = _securityUnitOfWork.UserRepository.GetByID(user.Id);
            if (entity == null)
                throw new ArgumentException("User is not found.", "user");

            return Task.FromResult(entity.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var entity = _securityUnitOfWork.UserRepository.GetByID(user.Id);
            if (entity == null)
                throw new ArgumentException("User is not found.", "user");

            return Task.FromResult(entity.IsLocked);
        }

        public Task SetLockoutEnabledAsync(ApplicationUser user, bool enabled)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var entity = _securityUnitOfWork.UserRepository.GetByID(user.Id);
            if (entity == null)
                throw new ArgumentException("User is not found.", "user");

            entity.IsLocked = enabled;
            _securityUnitOfWork.UserRepository.Update(entity);
            return _securityUnitOfWork.SaveAsync();
        }
        #endregion

        #region IUserPasswordStore
        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (string.IsNullOrEmpty(passwordHash))
                throw new ArgumentNullException("passwordHash");

            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            var entity = _securityUnitOfWork.UserRepository.GetByID(user.Id);
            if (entity == null)
                throw new ArgumentException("User is not found.", "user");
            bool hasPassword = !string.IsNullOrEmpty(entity.PasswordHash);
            return Task.FromResult(hasPassword);
        }
        #endregion

        #region IUserPhoneNumberStore
        public Task SetPhoneNumberAsync(ApplicationUser user, string phoneNumber)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (string.IsNullOrEmpty(phoneNumber))
                throw new ArgumentNullException("phoneNumber");

            var entity = _securityUnitOfWork.UserRepository.GetByID(user.Id);
            if (entity == null)
                throw new ArgumentException("User is not found.", "user");

            entity.PhoneNumber = phoneNumber;
            entity.IsPhoneConfirmed = false;

            _securityUnitOfWork.UserRepository.Update(entity);
            return _securityUnitOfWork.SaveAsync();
        }

        public Task<string> GetPhoneNumberAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var entity = _securityUnitOfWork.UserRepository.GetByID(user.Id);
            if (entity == null)
                throw new ArgumentException("User is not found.", "user");

            return Task.FromResult(entity.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var entity = _securityUnitOfWork.UserRepository.GetByID(user.Id);
            if (entity == null)
                throw new ArgumentException("User is not found.", "user");

            return Task.FromResult(entity.IsPhoneConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(ApplicationUser user, bool confirmed)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var entity = _securityUnitOfWork.UserRepository.GetByID(user.Id);
            if (entity == null)
                throw new ArgumentException("User is not found.", "user");

            entity.IsPhoneConfirmed = confirmed;

            _securityUnitOfWork.UserRepository.Update(entity);
            return _securityUnitOfWork.SaveAsync();
        }
        #endregion

        #region IUserSecurityStampStore
        public Task SetSecurityStampAsync(ApplicationUser user, string stamp)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (string.IsNullOrEmpty(stamp))
                throw new ArgumentNullException("stamp");

            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        public Task<string> GetSecurityStampAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var entity = _securityUnitOfWork.UserRepository.GetByID(user.Id);
            if (entity == null)
                throw new ArgumentException("User is not found.", "user");

            return Task.FromResult(entity.SecurityStamp);
        }
        #endregion

        #region IUserTwoFactorStore
        public Task SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var entity = _securityUnitOfWork.UserRepository.GetByID(user.Id);
            if (entity == null)
                throw new ArgumentException("User is not found.", "user");

            entity.EnableTowFactorAuthentication = enabled;
            _securityUnitOfWork.UserRepository.Update(entity);
            return _securityUnitOfWork.SaveAsync();
        }

        public Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var entity = _securityUnitOfWork.UserRepository.GetByID(user.Id);
            if (entity == null)
                throw new ArgumentException("User is not found.", "user");

            return Task.FromResult(entity.EnableTowFactorAuthentication);
        }
        #endregion

        #region IUserRoleStore
        public Task AddToRoleAsync(ApplicationUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (string.IsNullOrEmpty(roleName))
                throw new ArgumentNullException("roleName");

            var entity = _securityUnitOfWork.UserRepository.GetByID(user.Id);
            if (entity == null)
                throw new ArgumentException("User is not found.", "user");

            var role = _securityUnitOfWork.RoleRepository.FindByName(roleName);
            if (role == null)
                throw new ArgumentException("Role is not found.", "roleName");

            entity.AddRole(role);
            _securityUnitOfWork.UserRepository.Update(entity);
            return _securityUnitOfWork.SaveAsync();
        }

        public Task RemoveFromRoleAsync(ApplicationUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (string.IsNullOrEmpty(roleName))
                throw new ArgumentNullException("roleName");

            var entity = _securityUnitOfWork.UserRepository.GetByID(user.Id);
            if (entity == null)
                throw new ArgumentException("User is not found.", "user");

            var role = entity.Roles.FirstOrDefault(c => c.RoleName.ToLower() == roleName.ToLower());
            if (role != null)
                entity.RemoveRole(role);

            _securityUnitOfWork.UserRepository.Update(entity);
            return _securityUnitOfWork.SaveAsync();
        }

        public Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var entity = _securityUnitOfWork.UserRepository.GetByID(user.Id);
            if (entity == null)
                throw new ArgumentException("User is not found.", "user");
            IList<string> rolesName = entity.Roles == null ? null : entity.Roles.Select(c => c.RoleName).ToList();
            return Task.FromResult(rolesName);
        }

        public Task<bool> IsInRoleAsync(ApplicationUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (string.IsNullOrEmpty(roleName))
                throw new ArgumentNullException("roleName");

            var entity = _securityUnitOfWork.UserRepository.GetByID(user.Id);
            if (entity == null)
                throw new ArgumentException("User is not found.", "user");

            bool isUserInRole = entity.Roles.Any(c => c.RoleName.ToLower() == roleName.ToLower());
            return Task.FromResult(isUserInRole);
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UserStore()
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

        #region Private Methods
        private ApplicationUser GetApplicationUser(User user)
        {
            if (user == null)
                return null;

            var applicationUser = new ApplicationUser
            {
                AccessFailedCount = user.AccessFailedCount,
                Email = user.Email,
                EnableTowFactorAuthentication = user.EnableTowFactorAuthentication,
                FullName = user.FullName,
                IsEmailConfirmed = user.IsEmailConfirmed,
                IsLocked = user.IsLocked,
                IsPhoneConfirmed = user.IsPhoneConfirmed,
                LockedOutDate = user.LockedOutDate,
                PasswordHash = user.PasswordHash,
                PhoneNumber = user.PasswordHash,
                SecurityStamp = user.SecurityStamp,
                UserId = user.UserId,
                UserName = user.UserName,
            };

            foreach (var role in user.Roles)
                applicationUser.AddRole(role);

            foreach (var externalLogin in user.ExternalLogins)
                applicationUser.AddExternalLogin(externalLogin.LoginProvider, externalLogin.ProviderKey);

            foreach (var claim in user.Claims)
                applicationUser.AddClaim(claim.ClaimType, claim.ClaimValue);

            return applicationUser;
        }
        #endregion
    }
}
