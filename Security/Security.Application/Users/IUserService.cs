using BusinessSolutions.Common.Core;
using BusinessSolutions.Common.Infra.Validation;
using Sanabel.Security.Domain;
using Security.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sanabel.Security.Application
{
    public interface IUserService
    {
        Task<EntityResult> AddUser(UserViewModel userModel);

        PagedEntity<ViewUserViewModel> SearchUser(SearchUsersViewModel searchUserModel);

        Task<UserViewModel> GetUser(Guid id);

        Task<EntityResult> UpdateUser(UserViewModel userModel);

        List<Role> GetAllRoles();
    }
}
