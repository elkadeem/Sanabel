using BusinessSolutions.Common.Infra.Validation;
using Security.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Application.Users
{
    public interface IUserService
    {
        Task<EntityResult> AddUser(RegisterViewModel userModel);
    }
}
