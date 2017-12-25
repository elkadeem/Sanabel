using Microsoft.AspNet.Identity;
using Security.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.AspIdentity
{
    public class ApplicationRole : Role, IRole<Guid>
    {
        public string Name { get => this.RoleName; set => this.RoleName = value; }
    }
}
