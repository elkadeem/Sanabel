using Microsoft.AspNet.Identity;
using Sanabel.Security.Domain;
using System;

namespace Security.AspIdentity
{
    public class ApplicationRoleManager : RoleManager<Role, Guid>
    {
        public ApplicationRoleManager(IRoleStore<Role, Guid> roleStore)
               : base(roleStore)
        {
        }
    }
}
