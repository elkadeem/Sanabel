using BusinessSolutions.Common.Infra.Authentication;
using BusinessSolutions.Common.Infra.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Security.AspIdentity
{
    public class UserFactory : IUserFactory
    {
        private readonly ApplicationUserManager _userManager;
        public UserFactory(ApplicationUserManager userManager)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(userManager, nameof(_userManager));
            _userManager = userManager;
        }

        public string CurrentUserName
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                    return HttpContext.Current.User.Identity.Name;

                return "Anonymous";
            }
        }
        public Guid? CurrentUserId
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    string userName = HttpContext.Current.User.Identity.Name;
                    var getUserTask = _userManager.FindByNameAsync(userName);
                    getUserTask.Wait();
                    return getUserTask.Result?.Id;
                }

                return null;
            }
        }
    }
}

