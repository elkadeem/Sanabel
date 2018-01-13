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

            User newUser = new User();
            UpdateUserData(newUser, user);
            _securityUnitOfWork.UserRepository.Add(newUser);
            return _securityUnitOfWork.SaveAsync();
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            _securityUnitOfWork.UserRepository.Remove(user.Id);
            return _securityUnitOfWork.SaveAsync();
        }

        public Task<ApplicationUser> FindByIdAsync(Guid Id)
        {
            var user = _securityUnitOfWork.UserRepository.GetByID(Id);
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

            var entity = _securityUnitOfWork.UserRepository.GetByID(user.Id);
            if (entity == null)
                throw new ArgumentException("User is not found.", "user");

            UpdateUserData(entity, user);
            _securityUnitOfWork.UserRepository.Update(entity);
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

            user.AddExternalLogin(login.LoginProvider, login.ProviderKey);
            return Task.FromResult(0);
        }

        public async Task<ApplicationUser> FindAsync(UserLoginInfo login)
        {
            if (login == null)
                throw new ArgumentNullException("login");

            var user = await _securityUnitOfWork.UserRepository
                .FindByLoginAsync(login.LoginProvider, login.ProviderKey);
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

            user.RemoveExternalLogin(login.LoginProvider);
            return Task.FromResult(0);
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

            user.Email = email;
            return Task.FromResult(0);
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

            return Task.FromResult(user.IsEmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.IsEmailConfirmed = confirmed;
            return Task.FromResult(0);
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


            DateTimeOffset date = DateTimeOffset.MinValue;
            if (user.LockedOutDate.HasValue)
                date = new DateTimeOffset(user.LockedOutDate.Value);

            return Task.FromResult(date);
        }

        public Task SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset lockoutEnd)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (lockoutEnd == DateTimeOffset.MinValue)
                user.LockedOutDate = null;
            else
                user.LockedOutDate = lockoutEnd.UtcDateTime;

            return Task.FromResult(0);
        }

        public Task<int> IncrementAccessFailedCountAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.AccessFailedCount = user.AccessFailedCount + 1;
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task ResetAccessFailedCountAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        public Task<int> GetAccessFailedCountAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.IsLocked);
        }

        public Task SetLockoutEnabledAsync(ApplicationUser user, bool enabled)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.IsLocked = enabled;
            return Task.FromResult(0);
        }
        #endregion

        #region IUserPasswordStore
        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            if (user == null)
                throw new ArgumentNullException("user");

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

            bool hasPassword = !string.IsNullOrEmpty(user.PasswordHash);
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

            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public Task<string> GetPhoneNumberAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.IsPhoneConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(ApplicationUser user, bool confirmed)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.IsPhoneConfirmed = confirmed;
            return Task.FromResult(0);
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

            return Task.FromResult(user.SecurityStamp);
        }
        #endregion

        #region IUserTwoFactorStore
        public Task SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.EnableTowFactorAuthentication = enabled;
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.EnableTowFactorAuthentication);
        }
        #endregion

        #region IUserRoleStore
        public Task AddToRoleAsync(ApplicationUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (string.IsNullOrEmpty(roleName))
                throw new ArgumentNullException("roleName");

            var role = _securityUnitOfWork.RoleRepository.FindByName(roleName);
            if (role == null)
                throw new ArgumentException("Role is not found.", "roleName");

            user.AddRole(role);
            return Task.FromResult(0);
        }

        public Task RemoveFromRoleAsync(ApplicationUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (string.IsNullOrEmpty(roleName))
                throw new ArgumentNullException("roleName");

            if (user.Roles != null)
            {
                var role = user.Roles.FirstOrDefault(c => c.RoleName.ToLower() == roleName.ToLower());
                if (role != null)
                    user.RemoveRole(role);
            }

            return Task.FromResult(0);
        }

        public Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var entity = _securityUnitOfWork.UserRepository.GetByID(user.Id);
            if (entity == null)
                throw new ArgumentException("User is not found.", "user");
            IList<string> rolesName = entity.Roles?.Select(c => c.RoleName).ToList();
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
                PhoneNumber = user.PhoneNumber,
                SecurityStamp = user.SecurityStamp,
                Id = user.Id,
                UserName = user.UserName,
                CityId = user.CityId,
                DistrictId = user.DistrictId,
                Address = user.Address,
                City = user.City,
                District = user.District,
                Roles = user.Roles,
                ExternalLogins = user.ExternalLogins,
                Claims = user.Claims,
            };
           
            return applicationUser;
        }

        private void UpdateUserData(User user, ApplicationUser applicationUser)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (applicationUser == null)
                throw new ArgumentNullException("applicationUser");

            user.AccessFailedCount = applicationUser.AccessFailedCount;
            user.Email = applicationUser.Email;
            user.EnableTowFactorAuthentication = applicationUser.EnableTowFactorAuthentication;
            user.FullName = applicationUser.FullName;
            user.IsEmailConfirmed = applicationUser.IsEmailConfirmed;
            user.IsLocked = applicationUser.IsLocked;
            user.IsPhoneConfirmed = applicationUser.IsPhoneConfirmed;
            user.LockedOutDate = applicationUser.LockedOutDate;
            user.PasswordHash = applicationUser.PasswordHash;
            user.PhoneNumber = applicationUser.PhoneNumber;
            user.SecurityStamp = applicationUser.SecurityStamp;
            user.Id = applicationUser.Id;
            user.UserName = applicationUser.UserName;
            user.CityId = applicationUser.CityId;
            user.DistrictId = applicationUser.DistrictId;
            user.Address = applicationUser.Address;

            UpdateRoles(user, applicationUser);
            UpdateExternalLogn(user, applicationUser);
            UpdateClaims(user, applicationUser);
        }

        private static void UpdateClaims(User user, ApplicationUser applicationUser)
        {
            foreach (var claim in user.Claims.Where(c => !applicationUser.Claims.Any(e => e.ClaimId == c.ClaimId)).ToList())
            {
                user.Claims.Remove(claim);
            }

            foreach (var claim in applicationUser.Claims.Where(c => !user.Claims.Any(e => e.ClaimId == c.ClaimId)))
            {
                user.AddClaim(claim.ClaimType, claim.ClaimValue);
            }
        }

        private static void UpdateExternalLogn(User user, ApplicationUser applicationUser)
        {
            foreach (var externalLogin in user.ExternalLogins.Where(c => !applicationUser.ExternalLogins
                        .Any(e => e.ProviderKey == c.ProviderKey)).ToList())
            {
                user.ExternalLogins.Remove(externalLogin);
            }

            foreach (var externalLogin in applicationUser.ExternalLogins
                .Where(c => !user.ExternalLogins.Any(e => e.ProviderKey == c.ProviderKey)))
            {
                user.ExternalLogins.Add(externalLogin);
            }
        }

        private static void UpdateRoles(User user, ApplicationUser applicationUser)
        {
            foreach (var role in user.Roles.Where(c => !applicationUser.Roles.Any(e => e.Id == c.Id)).ToList())
            {
                user.Roles.Remove(role);
            }

            foreach (var role in applicationUser.Roles.Where(c => !user.Roles.Any(e => e.Id == c.Id)))
            {
                user.Roles.Add(role);
            }
        }
        #endregion
    }
}
