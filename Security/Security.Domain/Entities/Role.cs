using System;
using System.Collections.Generic;

namespace Security.Domain
{
    public class Role
    {
        public Guid RoleId { get; set; }

        public string RoleName { get; set; }

        public string RoleNameAr { get; set; }

        public List<User> Users { get; set; }
    }
}