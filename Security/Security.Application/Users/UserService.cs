using BusinessSolutions.Common.Core;
using BusinessSolutions.Common.Infra.Validation;
using Microsoft.AspNet.Identity;
using Security.Application.Localization;
using Security.Application.Models;
using Security.AspIdentity;
using Security.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Security.Application.Users
{
    public class UserService : IUserService
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private ISecurityUnitOfWork _securityUnitOfWork;
        public UserService(ApplicationUserManager userManager, ApplicationRoleManager roleManager
            , ISecurityUnitOfWork securityUnitOfWork)
        {
            if (userManager == null)
                throw new ArgumentNullException("userManager");
            if (roleManager == null)
                throw new ArgumentNullException("roleManager");
            if (securityUnitOfWork == null)
                throw new ArgumentNullException("securityUnitOfWork");

            _userManager = userManager;
            _roleManager = roleManager;
            _securityUnitOfWork = securityUnitOfWork;
        }

        public async Task<EntityResult> AddUser(VolunteerViewModel userModel)
        {
            try
            {
                using (TransactionScope transactionScop = new TransactionScope(TransactionScopeOption.RequiresNew
                    , TransactionScopeAsyncFlowOption.Enabled))
                {
                    var user = new ApplicationUser
                    {
                        UserName = userModel.Email,
                        Email = userModel.Email,
                        Address = userModel.Address,
                        CityId = userModel.CityId,
                        DistrictId = userModel.DistrictId,
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

        public List<ApplicationRole> GetAllRoles()
        {
            return _roleManager.Roles.ToList();
        }

        public async Task<VolunteerViewModel> GetUser(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new ArgumentException("User is not exist.", "userId");

            return new VolunteerViewModel
            {
                Address = user.Address,
                CityId = user.CityId,
                CountryId = user.City.Region.CountryId,
                DistrictId = user.DistrictId,
                Email = user.Email,
                FullName = user.FullName,
                Mobile = user.PhoneNumber,
                RegionId = user.City.RegionId,
                Roles = user.Roles?.Select(c => c.Id).ToList(),
                Id = user.Id,
            };
        }

        public PagedEntity<ViewVolunteerViewModel> SearchUser(SearchVolunteersViewModel searchUserModel)
        {
            if (searchUserModel == null)
                throw new ArgumentNullException("searchUserModel");

            PagedEntity<User> result = _securityUnitOfWork.UserRepository.SearchUsers(searchUserModel.UserName, searchUserModel.Email
                , searchUserModel.FullName, searchUserModel.CountryId, searchUserModel.RegionId
                , searchUserModel.CityId, searchUserModel.DistrictId
                , searchUserModel.PageIndex, searchUserModel.PageSize);

            return new PagedEntity<ViewVolunteerViewModel>(result.Items.Select(c => GetViewUserViewModel(c)), result.TotalCount);

        }

        public async Task<EntityResult> UpdateUser(VolunteerViewModel userModel)
        {
            if (userModel == null)
                throw new ArgumentNullException("userModel");
            var user = await _userManager.FindByIdAsync(userModel.Id);
            if (user == null)
                throw new ArgumentException("User is not found.", "userModel");

            user.Address = userModel.Address;
            user.CityId = userModel.CityId;
            user.DistrictId = userModel.DistrictId;
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
                        .Select(c => c.RoleName);
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

        private ViewVolunteerViewModel GetViewUserViewModel(User user)
        {
            if (user == null)
                return null;
            return new ViewVolunteerViewModel
            {
                Address = user.Address,
                CityName = user.City?.Name,
                CountryName = user.City?.Region?.Country?.Name,
                DistrictName = user.District?.Name,
                Email = user.Email,
                FullName = user.FullName,
                RegionName = user.City?.Region?.Name,
                UserId = user.Id,
                UserName = user.UserName,
                IsLockOut = user.IsLocked,
            };

        }
    }
}
