using BusinessSolutions.Common.Infra.Validation;
using Microsoft.AspNet.Identity;
using Security.Application.Models;
using Security.AspIdentity;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Security.Application.Users
{
    public class UserService : IUserService
    {
        public ApplicationUserManager _userManager;
        public ApplicationRoleManager _roleManager;
        public UserService(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            if (userManager == null)
                throw new ArgumentNullException("userManager");
            if (roleManager == null)
                throw new ArgumentNullException("roleManager");

            _userManager = userManager;
            _roleManager = roleManager;
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
                        transactionScop.Complete();

                    return GetEntityResult(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
    }
}
