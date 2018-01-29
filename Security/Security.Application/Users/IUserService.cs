using BusinessSolutions.Common.Core;
using BusinessSolutions.Common.Infra.Validation;
using Security.Application.Models;
using Security.AspIdentity;
using Security.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Application.Users
{
    public interface IUserService
    {
        Task<EntityResult> AddUser(VolunteerViewModel userModel);

        PagedEntity<ViewVolunteerViewModel> SearchUser(SearchVolunteersViewModel searchUserModel);

        Task<VolunteerViewModel> GetUser(Guid id);

        Task<EntityResult> UpdateUser(VolunteerViewModel userModel);

        List<Role> GetAllRoles();
    }
}
