using BusinessSolutions.Common.Core;
using BusinessSolutions.Common.Infra.Validation;
using Microsoft.AspNet.Identity;
using Sanabel.Security.Application.Localization;
using Sanabel.Security.Domain;
using Security.AspIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Sanabel.Security.Application
{
    public class UserService : IUserService
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private ISecurityUnitOfWork _securityUnitOfWork;
        public UserService(ApplicationUserManager userManager, ApplicationRoleManager roleManager
            , ISecurityUnitOfWork securityUnitOfWork)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(userManager, nameof(userManager));
            Guard.ArgumentIsNull<ArgumentNullException>(roleManager, nameof(roleManager));
            Guard.ArgumentIsNull<ArgumentNullException>(securityUnitOfWork, nameof(securityUnitOfWork));            
            _userManager = userManager;
            _roleManager = roleManager;
            _securityUnitOfWork = securityUnitOfWork;
        }

        public async Task<EntityResult> AddUser(UserViewModel userModel)
        {
            try
            {
                using (TransactionScope transactionScop = new TransactionScope(TransactionScopeOption.RequiresNew
                    , TransactionScopeAsyncFlowOption.Enabled))
                {
                    var user = new User
                    {
                        UserName = userModel.Email,
                        Email = userModel.Email,                        
                        PhoneNumber = userModel.Mobile,
                        FullName = userModel.FullName,
                    };

                    var result = await _userManager.CreateAsync(user, userModel.Password);
                    if (result.Succeeded)
                    {
                        if (userModel.Roles != null && userModel.Roles.Count > 0)
                        {
                            var rolesToAdd = _roleManager.Roles.Where(c => userModel.Roles.Contains(c.Id))
                                .Select(c => c.Name);
                            result = await _userManager.AddToRolesAsync(user.Id, rolesToAdd.ToArray());
                        }
                    }

                    if (result.Succeeded)
                    {
                        string emailConfirmationcode = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        string message = string.Format(SecurityResource.EmailVerificationEmailMessage, emailConfirmationcode);
                        await _userManager.SendEmailAsync(user.Id, SecurityResource.EmailVerificationEmailSubject, message);

                        transactionScop.Complete();
                        userModel.Id = user.Id;
                    }

                    return GetEntityResult(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Role> GetAllRoles()
        {
            return _roleManager.Roles.ToList();
        }

        public async Task<UserViewModel> GetUser(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new ArgumentException("User is not exist.", "userId");

            return new UserViewModel
            {
                
                Email = user.Email,
                FullName = user.FullName,
                Mobile = user.PhoneNumber,                
                Roles = user.Roles?.Select(c => c.Id).ToList(),
                Id = user.Id,
            };
        }

        public PagedEntity<ViewUserViewModel> SearchUser(SearchUsersViewModel searchUserModel)
        {
            if (searchUserModel == null)
                throw new ArgumentNullException("searchUserModel");

            PagedEntity<User> result = _securityUnitOfWork.UserRepository.SearchUsers(searchUserModel.UserName, searchUserModel.Email
                , searchUserModel.FullName
                , searchUserModel.PageIndex, searchUserModel.PageSize);

            return new PagedEntity<ViewUserViewModel>(result.Items.Select(c => GetViewUserViewModel(c)), result.TotalCount);

        }

        public async Task<EntityResult> UpdateUser(UserViewModel userModel)
        {
            if (userModel == null)
                throw new ArgumentNullException("userModel");
            var user = await _userManager.FindByIdAsync(userModel.Id);
            if (user == null)
                throw new ArgumentException("User is not found.", "userModel");
            
            user.Email = userModel.Email;
            user.FullName = userModel.FullName;
            user.PhoneNumber = userModel.Mobile;

            using (TransactionScope transactionScop = new TransactionScope(TransactionScopeOption.RequiresNew
                    , TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var rolesToRemove = user.Roles.Where(c => !userModel.Roles.Contains(c.Id))
                        .Select(c => c.Name);
                    if (rolesToRemove.Count() > 0)
                        result = await _userManager.RemoveFromRolesAsync(user.Id, rolesToRemove.ToArray());

                    if (result.Succeeded)
                    {
                        var rolesToAdd = _roleManager.Roles.Where(c => userModel.Roles.Contains(c.Id)
                           && !user.Roles.Any(e => e.Id == c.Id))
                            .Select(c => c.Name);

                        if (rolesToAdd.Count() > 0)
                            result = await _userManager.AddToRolesAsync(user.Id, rolesToAdd.ToArray());
                    }
                }

                if (result.Succeeded)
                {
                    transactionScop.Complete();
                }

                return GetEntityResult(result);
            }
        }

        private EntityResult GetEntityResult(IdentityResult result)
        {
            if (result == null)
                throw new ArgumentNullException("result");
            if (result.Succeeded)
                return EntityResult.Success;

            return EntityResult.Failed(result
                .Errors
                .Select(c => new ValidationError(c, ValidationErrorTypes.BusinessError))
                .ToArray());
        }

        private ViewUserViewModel GetViewUserViewModel(User user)
        {
            if (user == null)
                return null;
            return new ViewUserViewModel
            {                
                Email = user.Email,
                FullName = user.FullName,
                UserId = user.Id,
                UserName = user.UserName,
                IsLockOut = user.IsLocked,
            };

        }
    }
}
