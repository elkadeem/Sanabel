using BusinessSolutions.Common.Core;
using BusinessSolutions.Common.Infra.Log;
using BusinessSolutions.Common.Infra.Validation;
using Microsoft.AspNet.Identity;
using Sanabel.Security.Application.Localization;
using Sanabel.Security.Application.Models;
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
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationRoleManager _roleManager;        
        private readonly ISecurityUnitOfWork _securityUnitOfWork;
        private readonly ILogger _logger;
        public UserService(ApplicationUserManager userManager, ApplicationRoleManager roleManager
            , ISecurityUnitOfWork securityUnitOfWork, ILogger logger)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(userManager, nameof(userManager));
            Guard.ArgumentIsNull<ArgumentNullException>(roleManager, nameof(roleManager));
            Guard.ArgumentIsNull<ArgumentNullException>(securityUnitOfWork, nameof(securityUnitOfWork));
            _userManager = userManager;
            _roleManager = roleManager;
            _securityUnitOfWork = securityUnitOfWork;
            _logger = logger;
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
                        PhoneNumber = userModel.Phone,
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
                _logger.Error(ex);
                throw;
            }
        }

        public async Task<EntityResult> BlockUser(Guid userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
                IdentityResult result = await _userManager.SetLockoutEnabledAsync(user.Id, true);
                if (result.Succeeded)
                    result = await _userManager.SetLockoutEndDateAsync(user.Id, DateTime.Now.Add(TimeSpan.FromDays(365 * 100)));

                return GetEntityResult(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
            
        }

        public async Task<EntityResult> ChangePassword(Guid userId, ChangePasswordViewModel model)
        {
            try
            {
                var result = await _userManager.ChangePasswordAsync(userId, model.OldPassword, model.NewPassword);
                return GetEntityResult(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
            
        }

        public List<Role> GetAllRoles()
        {
            return _roleManager.Roles.ToList();
        }

        public async Task<ViewUserViewModel> GetUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new ArgumentException("User is not exist.", nameof(id));

            return GetViewUserViewModel(user);
        }

        public async Task<EntityResult> ResetUserPassword(Guid userId, SetPasswordViewModel model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
                string token = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
                var result = await _userManager.ResetPasswordAsync(user.Id, token, model.NewPassword);
                return GetEntityResult(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }            
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

        public async Task<EntityResult> UnBlockUser(Guid id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
                IdentityResult result = await _userManager.SetLockoutEnabledAsync(user.Id, false);
                if (result.Succeeded)
                    result = await _userManager.SetLockoutEndDateAsync(user.Id, DateTimeOffset.MinValue);
                return GetEntityResult(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }

        public async Task<EntityResult> UpdateUser(EditUserViewModel userModel)
        {
            if (userModel == null)
                throw new ArgumentNullException("userModel");
            var user = await _userManager.FindByIdAsync(userModel.Id);
            if (user == null)
                throw new ArgumentException("User is not found.", "userModel");
            
            user.FullName = userModel.FullName;
            user.PhoneNumber = userModel.Phone;            
            user.IsLocked = userModel.IsLockOut;
            if (!user.IsLocked && user.LockedOutDate.HasValue)
                user.LockedOutDate = null;
            if (user.IsLocked && (!user.LockedOutDate.HasValue
                || user.LockedOutDate.Value == DateTime.MinValue))
                user.LockedOutDate = DateTime.Now.Add(TimeSpan.FromDays(365 * 100));

            using (TransactionScope transactionScop = new TransactionScope(TransactionScopeOption.RequiresNew
                    , TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var rolesToRemove = user.Roles.Where(c => !userModel.Roles.Contains(c.Id))
                        .Select(c => c.Name);
                    if (rolesToRemove.Any())
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
                .Select(c => new EntityError(c, ValidationErrorTypes.BusinessError))
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
                Phone = user.PhoneNumber,
                Roles = user.Roles?.ToDictionary(c => c.Id, c => c.NameAr)
            };

        }
    }
}
