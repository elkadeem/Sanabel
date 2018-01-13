using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.AspIdentity
{
    public class ApplicationUserManager : UserManager<ApplicationUser, Guid>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser, Guid> store)
            : base(store)
        {
            ConfigureUserManager();
        }

        private void ConfigureUserManager()
        {
            this.UserValidator = new UserValidator<ApplicationUser, Guid>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true,                
            };
                       

            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            this.UserLockoutEnabledByDefault = true;
            this.MaxFailedAccessAttemptsBeforeLockout = 5;
            // if you want to lock out indefinitely 200 years should be enough
            this.DefaultAccountLockoutTimeSpan = TimeSpan.FromDays(365 * 100);

        }
    }
}
