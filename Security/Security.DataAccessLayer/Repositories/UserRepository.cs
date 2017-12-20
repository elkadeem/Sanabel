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
    public class UserRepository : BaseEntityFrameworkRepository<Guid, User, Domain.User>, IUserRepository
    {
        public UserRepository(SecurityContext securityContext) : base(securityContext)
        {

        }

        public Domain.User FindByEmail(string email)
        {
            var user = Set.FirstOrDefault(c => c.Email == email);
            return GetDomainEntity(user);
        }

        public async Task<Domain.User> FindByEmailAsync(string email)
        {
            var user = await Set.FirstOrDefaultAsync(c => c.Email == email);
            return GetDomainEntity(user);
        }

        public async Task<Domain.User> FindByEmailAsync(CancellationToken cancellationToken, string email)
        {
            var user = await Set.FirstOrDefaultAsync(c => c.Email == email, cancellationToken);
            return GetDomainEntity(user);
        }

        public async Task<Domain.User> FindByLoginAsync(string loginProvider, string loginKey)
        {
            var user = await Set.FirstOrDefaultAsync(c => c.UserLogins.Any(e => e.LoginProvider == loginProvider
             && e.ProviderKey == loginKey));

            return GetDomainEntity(user);
        }

        public Domain.User FindByUserName(string userName)
        {
            var user = Set.FirstOrDefault(c => c.UserName == userName);
            return GetDomainEntity(user);
        }

        public async Task<Domain.User> FindByUserNameAsync(string userName)
        {
            var user = await Set.FirstOrDefaultAsync(c => c.UserName == userName);
            return GetDomainEntity(user);
        }

        public async Task<Domain.User> FindByUserNameAsync(CancellationToken cancellationToken, string userName)
        {
            var user = await Set.FirstOrDefaultAsync(c => c.UserName == userName, cancellationToken);
            return GetDomainEntity(user);
        }

        public override Domain.User GetDomainEntity(User entity)
        {
            if (entity == null)
                return null;

            var user = new Domain.User
            {
                AccessFailedCount = entity.AccessFailedCount,
                Email = entity.Email,
                EnableTowFactorAuthentication = entity.EnableTowFactorAuthentication,
                FullName = entity.FullName,
                IsEmailConfirmed = entity.IsEmailConfirmed,
                IsLocked = entity.IsLocked,
                IsPhoneConfirmed = entity.IsPhoneConfirmed,
                LockedOutDate = entity.LockedOutDate,
                PasswordHash = entity.PasswordHash,
                PhoneNumber = entity.PhoneNumber,
                SecurityStamp = entity.SecurityStamp,
                UserId = entity.UserId,
                UserName = entity.UserName,
            };

            foreach (var role in entity.Roles)
            {
                user.AddRole(new Domain.Role
                {
                    RoleId = role.RoleId,
                    RoleName = role.RoleName,
                    RoleNameAr = role.RoleNameAr
                });
            }

            foreach (var claim in entity.Claims)
            {
                user.AddClaim(claim.ClaimType, claim.ClaimValue);
            }

            foreach (var userLogin in entity.UserLogins)
            {
                user.AddExternalLogin(userLogin.LoginProvider, userLogin.ProviderKey);
            }

            return user;
        }

        public override User GetEntity(Domain.User domainEntity)
        {
            if (domainEntity == null)
                return null;

            var user = Set.Local.FirstOrDefault(c => c.UserId == domainEntity.UserId);
            if (user == null)
                user = new User
                {
                    UserId = domainEntity.UserId,
                    UserName = domainEntity.UserName,
                };

            user.AccessFailedCount = domainEntity.AccessFailedCount;
            user.Email = domainEntity.Email;
            user.EnableTowFactorAuthentication = domainEntity.EnableTowFactorAuthentication;
            user.FullName = domainEntity.FullName;
            user.IsEmailConfirmed = domainEntity.IsEmailConfirmed;
            user.IsLocked = domainEntity.IsLocked;
            user.IsPhoneConfirmed = domainEntity.IsPhoneConfirmed;
            user.LockedOutDate = domainEntity.LockedOutDate;
            user.PasswordHash = domainEntity.PasswordHash;
            user.PhoneNumber = domainEntity.PhoneNumber;
            user.SecurityStamp = domainEntity.SecurityStamp;

            foreach (var role in domainEntity.Roles)
            {
                user.Roles.Add(new Role
                {
                    RoleId = role.RoleId,
                    RoleName = role.RoleName,
                    RoleNameAr = role.RoleNameAr
                });
            }

            foreach (var claim in domainEntity.Claims)
            {
                if (!user.Claims.Any(c => c.ClaimType == claim.ClaimType && c.ClaimValue == claim.ClaimValue))
                    user.Claims.Add(new UserClaim
                    {
                        ClaimType = claim.ClaimType,
                        ClaimValue = claim.ClaimValue,
                    });
            }

            foreach (var userLogin in domainEntity.ExternalLogins)
            {
                if (!user.UserLogins.Any(c => c.LoginProvider == userLogin.LoginProvider))
                    user.UserLogins.Add(new UserLogin
                    {
                        LoginProvider = userLogin.LoginProvider,
                        ProviderKey = userLogin.ProviderKey,
                    });
            }

            return user;
        }

    }
}
