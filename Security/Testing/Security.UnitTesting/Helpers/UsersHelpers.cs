using Security.AspIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.UnitTesting.Helpers
{
    public class UsersHelpers
    {
        public List<ApplicationUser> users = new List<ApplicationUser> {
            new ApplicationUser
            {
                Email = "elkadeem@hotmail.com",
                FullName = "elkadeem",
                Id = Guid.NewGuid(),
                UserName = "elkadeem",
                
            },
        };
    }
}
