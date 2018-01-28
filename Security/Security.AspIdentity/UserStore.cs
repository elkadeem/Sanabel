using BusinessSolutions.Common.Infra.Validation;
using Microsoft.AspNet.Identity;
using Security.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.AspIdentity
{
    public class UserStore : IUserStore<User, Guid>, IUserLoginStore<User, Guid>
        , IUserClaimStore<User, Guid>, IUserEmailStore<User, Guid>
        , IUserLockoutStore<User, Guid>, IUserPasswordStore<User, Guid>
        , IUserPhoneNumberStore<User, Guid>, IUserSecurityStampStore<User, Guid>
        , IUserTwoFactorStore<User, Guid>, IUserRoleStore<User, Guid>
    {
        private ISecurityUnitOfWork _securityUnitOfWork;

        public UserStore(ISecurityUnitOfWork securityUnitOfWork)
        {
            _securityUnitOfWork = securityUnitOfWork;
        }

        #region IUserStore
        public Task CreateAsync(User user)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            _securityUnitOfWork.UserRepository.Add(user);
            return _securityUnitOfWork.SaveAsync();
        }

        public Task DeleteAsync(User user)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            _securityUnitOfWork.UserRepository.Remove(user.Id);
            return _securityUnitOfWork.SaveAsync();
        }

        public Task<User> FindByIdAsync(Guid Id)
        {
            var user = _securityUnitOfWork.UserRepository.GetByID(Id);           
            return Task.FromResult(user);
        }

        public Task<User> FindByNameAsync(string userName)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(userName, nameof(userName));
            return _securityUnitOfWork.UserRepository.FindByUserNameAsync(userName);
        }

        public Task UpdateAsync(User user)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));            
            _securityUnitOfWork.UserRepository.Update(user);
            return _securityUnitOfWork.SaveAsync();
        }
        #endregion

        #region IUserLoginStore
        public Task AddLoginAsync(User user, UserLoginInfo login)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            Guard.ArgumentIsNull<ArgumentNullException>(login, nameof(login));

            user.AddExternalLogin(login.LoginProvider, login.ProviderKey);
            return Task.FromResult(0);
        }

        public async Task<User> FindAsync(UserLoginInfo login)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(login, nameof(login));
            var user = await _securityUnitOfWork.UserRepository
                .FindByLoginAsync(login.LoginProvider, login.ProviderKey);
            return user;
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(User user)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));             
            IList<UserLoginInfo>  result = user.ExternalLogins?
                .Select(c => new UserLoginInfo(c.LoginProvider, c.ProviderKey))
                .ToList();

            return Task.FromResult(result);
        }

        public Task RemoveLoginAsync(User user, UserLoginInfo login)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            Guard.ArgumentIsNull<ArgumentNullException>(login, nameof(login));
            user.RemoveExternalLogin(login.LoginProvider);
            return Task.FromResult(0);
        }
        #endregion 

        #region IUserClaimStore
        public Task<IList<System.Security.Claims.Claim>> GetClaimsAsync(User user)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            IList<System.Security.Claims.Claim> claims = user.Claims.Select(c => new System.Security.Claims.Claim(c.ClaimType, c.ClaimValue)).ToList();
            return Task.FromResult(claims);
        }

        public Task AddClaimAsync(User user, System.Security.Claims.Claim claim)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            Guard.ArgumentIsNull<ArgumentNullException>(claim, nameof(claim));
            user.AddClaim(claim.Type, claim.Value);
            return Task.FromResult(0);
        }

        public Task RemoveClaimAsync(User user, System.Security.Claims.Claim claim)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            Guard.ArgumentIsNull<ArgumentNullException>(claim, nameof(claim));
            user.RemoveClaim(claim.Type, claim.Value);
            return Task.FromResult(0);
        }
        #endregion

        #region IUserEmailStore
        public Task SetEmailAsync(User user, string email)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            Guard.StringIsNull<ArgumentNullException>(email, nameof(email));
            user.Email = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(User user)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            return Task.FromResult(user.IsEmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            user.IsEmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task<User> FindByEmailAsync(string email)
        {
            Guard.StringIsNull<ArgumentNullException>(email, nameof(email));
            var user = _securityUnitOfWork.UserRepository.FindByEmail(email);           
            return Task.FromResult(user);
        }
        #endregion

        #region IUserLockoutStore
        public Task<DateTimeOffset> GetLockoutEndDateAsync(User user)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            DateTimeOffset date = DateTimeOffset.MinValue;
            if (user.LockedOutDate.HasValue)
                date = new DateTimeOffset(user.LockedOutDate.Value);
            return Task.FromResult(date);
        }

        public Task SetLockoutEndDateAsync(User user, DateTimeOffset lockoutEnd)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            if (lockoutEnd == DateTimeOffset.MinValue)
                user.LockedOutDate = null;
            else
                user.LockedOutDate = lockoutEnd.UtcDateTime;
            return Task.FromResult(0);
        }

        public Task<int> IncrementAccessFailedCountAsync(User user)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            user.AccessFailedCount = user.AccessFailedCount + 1;
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task ResetAccessFailedCountAsync(User user)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            user.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        public Task<int> GetAccessFailedCountAsync(User user)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(User user)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            return Task.FromResult(user.IsLocked);
        }

        public Task SetLockoutEnabledAsync(User user, bool enabled)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            user.IsLocked = enabled;
            return Task.FromResult(0);
        }
        #endregion

        #region IUserPasswordStore
        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            bool hasPassword = !string.IsNullOrEmpty(user.PasswordHash);
            return Task.FromResult(hasPassword);
        }
        #endregion

        #region IUserPhoneNumberStore
        public Task SetPhoneNumberAsync(User user, string phoneNumber)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            Guard.StringIsNull<ArgumentNullException>(phoneNumber, nameof(phoneNumber));
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public Task<string> GetPhoneNumberAsync(User user)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(User user)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            return Task.FromResult(user.IsPhoneConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(User user, bool confirmed)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            user.IsPhoneConfirmed = confirmed;
            return Task.FromResult(0);
        }
        #endregion

        #region IUserSecurityStampStore
        public Task SetSecurityStampAsync(User user, string stamp)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            Guard.StringIsNull<ArgumentNullException>(stamp, nameof(stamp));
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        public Task<string> GetSecurityStampAsync(User user)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            return Task.FromResult(user.SecurityStamp);
        }
        #endregion

        #region IUserTwoFactorStore
        public Task SetTwoFactorEnabledAsync(User user, bool enabled)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            user.EnableTowFactorAuthentication = enabled;
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(User user)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            return Task.FromResult(user.EnableTowFactorAuthentication);
        }
        #endregion

        #region IUserRoleStore
        public Task AddToRoleAsync(User user, string roleName)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            Guard.StringIsNull<ArgumentNullException>(roleName, nameof(roleName));

            var role = _securityUnitOfWork.RoleRepository.FindByName(roleName);
            Guard.ArgumentIsNull<ArgumentNullException>(role, nameof(roleName), "Role is not exist");
            user.AddRole(role);
            return Task.FromResult(0);
        }

        public Task RemoveFromRoleAsync(User user, string roleName)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            Guard.StringIsNull<ArgumentNullException>(roleName, nameof(roleName));
            user.RemoveRole(roleName);
            return Task.FromResult(0);
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            IList<string> rolesName = user.Roles?.Select(c => c.Name).ToList();
            return Task.FromResult(rolesName);
        }

        public Task<bool> IsInRoleAsync(User user, string roleName)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            Guard.StringIsNull<ArgumentNullException>(roleName, nameof(roleName));
            return Task.FromResult(user.IsInRole(roleName));
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
    }
}
