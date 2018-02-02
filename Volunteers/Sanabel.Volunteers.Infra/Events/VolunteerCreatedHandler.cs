using BusinessSolutions.Common.Core.Events;
using Microsoft.AspNet.Identity;
using Sanabel.Security.Domain;
using Sanabel.Volunteers.Domain.Events;
using System;

namespace Sanabel.Volunteers.Infra.Events
{
    public class VolunteerCreatedHandler : IHandles<VolunteerCreated>
    {
        private UserManager<User, Guid> _userManager;
        public VolunteerCreatedHandler(UserManager<User, Guid> userManager)
        {
            _userManager = userManager;
        }

        public void Handle(VolunteerCreated args)
        {
            if (args != null)
            {
                var user = new User
                {
                    Email = args.Email,
                    FullName = args.Name,
                    UserName = args.Email,
                    PhoneNumber = args.Phone,
                };

                var identityResult = _userManager.Create(user);
            }
        }
    }
}
