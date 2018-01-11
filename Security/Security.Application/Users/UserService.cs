using BusinessSolutions.Common.Core;
using BusinessSolutions.Common.Infra.Validation;
using Microsoft.AspNet.Identity;
using Security.Application.Models;
using Security.AspIdentity;
using Security.Domain;
using System;
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

        public async Task<EntityResult> AddUser(RegisterViewModel userModel)
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
                        transactionScop.Complete();
                    }

                    return GetEntityResult(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RegisterViewModel> GetUser(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new ArgumentException("User is not exist.", "userId");

            return new RegisterViewModel
            {
                Address = user.Address,
                CityId = user.CityId,
                CountryId = user.City.Region.CountryId,
                DistrictId = user.DistrictId,
                Email = user.Email,
                FullName = user.FullName,
                Mobile = user.PhoneNumber,
                RegionId = user.City.RegionId,
                Roles = user.Roles?.Select(c => c.Id).ToList()
            };
        }

        public PagedEntity<ViewUserViewModel> SearchUser(SearchUsersViewModel searchUserModel)
        {
            if (searchUserModel == null)
                throw new ArgumentNullException("searchUserModel");

            PagedEntity<User> result = _securityUnitOfWork.UserRepository.SearchUsers(searchUserModel.UserName, searchUserModel.Email
                , searchUserModel.FullName, searchUserModel.CountryId, searchUserModel.RegionId
                , searchUserModel.CityId, searchUserModel.DistrictId
                , searchUserModel.PageIndex, searchUserModel.PageSize);

            return new PagedEntity<ViewUserViewModel>(result.Items.Select(c => GetViewUserViewModel(c)), result.TotalCount);

        }

        public async Task<EntityResult> UpdateUser(RegisterViewModel userModel)
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

            _userManager.UpdateAsync(user);
            foreach(var role in user.Roles.ToList())
            {
                if(userModel.Roles.Any(c => c == role.Id))
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
