using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Sanabel.Security.Domain;
using System;

namespace Security.AspIdentity
{
    public class ApplicationUserManager : UserManager<User, Guid>
    {
        public ApplicationUserManager(IUserStore<User, Guid> store
            , EmailService emailService)
    : base(store)
        {
            ConfigureUserManager(emailService);
        }

        private void ConfigureUserManager(EmailService emailService)
        {
            this.UserValidator = new UserValidator<User, Guid>(this)
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

            this.UserTokenProvider = new DataProtectorTokenProvider<User, Guid>
                (new MachineKeyProtectionProvider().Create("Sanabel Identity"));

            this.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<User, Guid>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });

        }
    }
}
