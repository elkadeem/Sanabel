using BusinessSolutions.Common.Core.Entities;
using System;
using System.Collections.Generic;

namespace Security.Domain
{
    public class Role : Entity<Guid>
    {
        public string RoleName { get; set; }

        public string RoleNameAr { get; set; }

        public List<User> Users { get; set; }
    }
}