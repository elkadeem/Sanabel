using Microsoft.AspNet.Identity;
using Security.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.AspIdentity
{
    public class ApplicationUser : User, IUser<Guid>
    {
        public User GetUser()
        {
            return new User
            {
                AccessFailedCount = this.AccessFailedCount,
                Address = this.Address,
                CityId = this.CityId,
                DistrictId = this.DistrictId,
                Email = this.Email,
                EnableTowFactorAuthentication = this.EnableTowFactorAuthentication,
                FullName = this.FullName,
                Id = this.Id,
                IsEmailConfirmed = this.IsEmailConfirmed,
                IsLocked = this.IsLocked,
                IsPhoneConfirmed = this.IsLocked,
                LockedOutDate = this.LockedOutDate,
                PasswordHash = this.PasswordHash,
                PhoneNumber = this.PhoneNumber,
                SecurityStamp = this.SecurityStamp,
                UserName = this.UserName
            };
        }
    }
}
