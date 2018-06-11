using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.Infra.Authentication
{
    public interface IUserFactory
    {
        string CurrentUserName { get; }

        Guid? CurrentUserId { get; }
    }
}
