using Sanabel.Security.Domain;
using System;
using System.Collections.Generic;

namespace Security.UnitTesting.Helpers
{
    public class UsersHelpers
    {
        public List<User> users = new List<User> {
            new User
            {
                Email = "elkadeem@hotmail.com",
                FullName = "elkadeem",
                Id = Guid.NewGuid(),
                UserName = "elkadeem",
                
            },
        };
    }
}
