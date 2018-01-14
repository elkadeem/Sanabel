using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Security.AspIdentity
{
    public class ApplicationUserManager : UserManager<ApplicationUser, Guid>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser, Guid> store
            , EmailService emailService)
    : base(store)
        {
            ConfigureUserManager(emailService);
        }

        private void ConfigureUserManager(EmailService emailService)
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
            this.DefaultAccountLockoutTimeSpan = TimeSpan.FromDays(365 * 100);

            if (emailService != null)
            {
                this.EmailService = emailService;
            }

            this.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser, Guid>
                (new MachineKeyProtectionProvider().Create("Sanabel Identity"));

            this.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser, Guid>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });

        }
    }
}
