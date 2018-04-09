using BusinessSolutions.Common.Core.Entities;
using BusinessSolutions.Common.Infra.Validation;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanabel.Security.Domain
{
    public class User : AggregateRoot, IUser<Guid>
    {
        private string _userName;
        private string _email;

        private User()
        {
            
            ExternalLogins = new HashSet<ExternalLogin>();
            Claims = new HashSet<Claim>();
            Roles = new HashSet<Role>();
        }

        public User(string userName, string email) : this()
        {
            Guard.StringIsNull<ArgumentNullException>(userName, nameof(userName));
            Guard.StringIsNull<ArgumentNullException>(email, nameof(email));

            Id = Guid.NewGuid();
            UserName = userName;
            Email = email; 
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                Guard.StringIsNull<ArgumentOutOfRangeException>(value, nameof(UserName));
                _userName = value;
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                Guard.StringIsNull<ArgumentOutOfRangeException>(value, nameof(Email));
                _email = value;
            }
        }

        public string FullName { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsPhoneConfirmed { get; set; }

        public bool IsLocked { get; set; }

        public DateTime? LockedOutDate { get; set; }

        public int AccessFailedCount { get; set; }

        public bool EnableTowFactorAuthentication { get; set; }

        public ICollection<Claim> Claims { get; set; }

        public ICollection<ExternalLogin> ExternalLogins { get; set; }

        public ICollection<Role> Roles { get; set; }

        public void AddExternalLogin(string loginProvider, string providerkey)
        {
            ExternalLogins.Add(new ExternalLogin(this.Id, loginProvider, providerkey));
        }

        public void RemoveExternalLogin(string loginProvider)
        {
            var externalLogin = ExternalLogins.FirstOrDefault(c => c.LoginProvider == loginProvider);
            Guard.ArgumentIsNull<ArgumentNullException>(externalLogin, nameof(loginProvider), "Login provider is not exist");
            ExternalLogins.Remove(externalLogin);
        }

        public void AddClaim(string claimType, string claimValue)
        {
            var claim = new Claim(this.Id, claimType, claimValue);
            Claims.Add(claim);
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
            Roles.Add(role);
        }

        public void RemoveRole(string roleName)
        {
            var roleToRemove = Roles.FirstOrDefault(c => c.Name == roleName);
            Roles.Remove(roleToRemove);
        }

        public bool IsInRole(string roleName)
        {
            return Roles.Any(c => c.Name.ToLower() == roleName.ToLower());
        }

    }
}
