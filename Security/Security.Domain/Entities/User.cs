using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Domain
{
    public class User
    {
        private List<ExternalLogin> _externalLogins;
        private List<Claim> _claims;
        private List<Role> _roles;

        public User()
        {
            _externalLogins = new List<ExternalLogin>();
            _claims = new List<Claim>();
            _roles = new List<Role>();
        }

        public Guid UserId { get; set; }

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

        public IReadOnlyList<Claim> Claims => _claims.AsReadOnly();

        public IReadOnlyList<ExternalLogin> ExternalLogins => _externalLogins.AsReadOnly();

        public IReadOnlyList<Role> Roles => _roles.AsReadOnly();

        public bool EnableTowFactorAuthentication { get; set; }

        public void AddExternalLogin(string loginProvider, string providerkey)
        {
            var externalLogin = _externalLogins.FirstOrDefault(c => c.LoginProvider == loginProvider);
            if (externalLogin == null)
            {
                externalLogin = new ExternalLogin() { LoginProvider = loginProvider };
                _externalLogins.Add(externalLogin);
            }

            externalLogin.ProviderKey = providerkey;
        }

        public void RemoveExternalLogin(string loginProvider)
        {
            var externalLogin = _externalLogins.FirstOrDefault(c => c.LoginProvider == loginProvider);
            if (externalLogin != null)
                _externalLogins.Remove(externalLogin);
        }

        public void AddClaim(string claimType, string claimValue)
        {
            var claim = _claims.FirstOrDefault(c => c.ClaimType == claimType && c.ClaimValue == claimValue);
            if (claim == null)
            {
                claim = new Claim { ClaimType = claimType };
                _claims.Add(claim);
            }

            claim.ClaimValue = claimValue;
        }

        public void RemoveClaim(string claimType, string claimValue)
        {
            var claims = _claims.Where(c => c.ClaimType == claimType && c.ClaimValue == claimValue);
            foreach (var claim in claims.ToList())
                _claims.Remove(claim);
        }

        public void RemoveClaims(string claimType)
        {
            var claims = _claims.Where(c => c.ClaimType == claimType);
            foreach (var claim in claims.ToList())
            {
                _claims.Remove(claim);
            }
        }

        public void AddRole(Role role)
        {
            if (!_roles.Any(c => c.RoleName == role.RoleName))
                _roles.Add(role);
        }

        public void RemoveRole(Role role)
        {
            var roleToRemove = _roles.FirstOrDefault(c => c.RoleName == role.RoleName);
            if (roleToRemove != null)
                _roles.Remove(roleToRemove);
        }
    }
}
