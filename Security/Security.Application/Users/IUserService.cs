using BusinessSolutions.Common.Core;
using BusinessSolutions.Common.Infra.Validation;
using Microsoft.AspNet.Identity;
using Sanabel.Security.Application.Models;
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
        
        Task<EntityResult> ChangePassword(Guid userId, ChangePasswordViewModel model);

        Task<EntityResult> ResetUserPassword(Guid userId, SetPasswordViewModel model);

        Task<EntityResult> BlockUser(Guid userId);

        Task<EntityResult> UnBlockUser(Guid id);
    }
}
