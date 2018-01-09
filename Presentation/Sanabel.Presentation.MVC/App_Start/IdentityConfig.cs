using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Security.AspIdentity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sanabel.Presentation.MVC
{
    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, Guid>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager
            , IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return this.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);            
        }
    }
}
