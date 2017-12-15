using Microsoft.AspNet.Identity;
using Security.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.AspIdentity
{
    public class ApplicationUser : User, IUser<Guid>
    {
        public Guid Id { get => this.UserId; private set => this.UserId = value; }
    }
}
