using BusinessSolutions.Common.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Domain
{
    public class User : Entity<Guid>
    {
        public User()
        {
            Id = Guid.NewGuid();
            ExternalLogins = new HashSet<ExternalLogin>();
            Claims = new HashSet<Claim>();
            Roles = new HashSet<Role>();
        }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsPhoneConfirmed { get; set; }

        public bool IsLocked { get; set; }

        public DateTime? LockedOutDate { get; set; }

        public int AccessFailedCount { get; set; }

        public bool EnableTowFactorAuthentication { get; set; }

        public int CityId { get; set; }

        public int? DistrictId { get; set; }

        public string Address { get; set; }

        public ICollection<Claim> Claims { get; set; }

        public ICollection<ExternalLogin> ExternalLogins { get; set; }

        public ICollection<Role> Roles { get; set; }
        
        public City City { get; set; }

        public District District { get; set; }

        public void AddExternalLogin(string loginProvider, string providerkey)
        {
            var externalLogin = ExternalLogins.FirstOrDefault(c => c.LoginProvider == loginProvider);
            if (externalLogin == null)
            {
                externalLogin = new ExternalLogin() { LoginProvider = loginProvider };
                ExternalLogins.Add(externalLogin);
            }

            externalLogin.ProviderKey = providerkey;
        }

        public void RemoveExternalLogin(string loginProvider)
        {
            var externalLogin = ExternalLogins.FirstOrDefault(c => c.LoginProvider == loginProvider);
            if (externalLogin != null)
                ExternalLogins.Remove(externalLogin);
        }

        public void AddClaim(string claimType, string claimValue)
        {
            var claim = Claims.FirstOrDefault(c => c.ClaimType == claimType && c.ClaimValue == claimValue);
            if (claim == null)
            {
                claim = new Claim { ClaimType = claimType };
                Claims.Add(claim);
            }

            claim.ClaimValue = claimValue;
        }

        public void RemoveClaim(string claimType, string claimValue)
        {
            var claims = Claims.Where(c => c.ClaimType == claimType && c.ClaimValue == claimValue);
            foreach (var claim in claims.ToList())
                Claims.Remove(claim);
        }

        public void RemoveClaims(string claimType)
        {
            var claims = Claims.Where(c => c.ClaimType == claimType);
            foreach (var claim in claims.ToList())
            {
                Claims.Remove(claim);
            }
        }

        public void AddRole(Role role)
        {
            if (!Roles.Any(c => c.RoleName == role.RoleName))
                Roles.Add(role);
        }

        public void RemoveRole(Role role)
        {
            var roleToRemove = Roles.FirstOrDefault(c => c.RoleName == role.RoleName);
            if (roleToRemove != null)
                Roles.Remove(roleToRemove);
        }
    }
}
