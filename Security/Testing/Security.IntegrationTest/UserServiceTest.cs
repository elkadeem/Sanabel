using NUnit.Framework;
using Security.Application.Models;
using Security.Application.Users;
using Security.AspIdentity;
using Security.DataAccessLayer.UnitOfWork;
using Security.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.IntegrationTest
{
    [TestFixture]
    public class UserServiceTest
    {
        private Security.AspIdentity.ApplicationUserManager userManager;
        private User user;
        private SecurityUnitOfWork _securityUnitOfWork;
        private IUserService _userService;
        private ApplicationRoleManager _roleManager;

        [OneTimeSetUp]
        public void Initiate()
        {
            var dataContext = new Security.DataAccessLayer.SecurityContext();
            _securityUnitOfWork = new Security.DataAccessLayer.UnitOfWork.SecurityUnitOfWork(dataContext);
            var userStore = new AspIdentity.UserStore(_securityUnitOfWork);

            userManager = new ApplicationUserManager(userStore, null)
            {
                UserLockoutEnabledByDefault = false,
                MaxFailedAccessAttemptsBeforeLockout = 3,
                DefaultAccountLockoutTimeSpan = new TimeSpan(0, 10, 0),

            };

            _roleManager = new ApplicationRoleManager(new RoleStore(_securityUnitOfWork));
            _userService = new UserService(userManager, _roleManager, _securityUnitOfWork);
        }

        [Test]
        public async Task AddUser_WithRole_AddUserAndUserRoles()
        {
            var user = new VolunteerViewModel()
            {
                Address = "Address",
                CityId = _securityUnitOfWork.CityRepository.GetAll().First().Id,
                ConfirmPassword = "P@ssw0rd",
                Email = "AddUser@Email.com",
                FullName = "AddUser",
                Mobile = "0123456",
                Password = "P@ssw0rd",
                Roles = _roleManager.Roles.Take(2).Select(c => c.Id).ToList()
            };

            var entityResult = await _userService.AddUser(user);
            Assert.IsTrue(entityResult.Succeeded);

            var newUser = await userManager.FindByNameAsync(user.Email);
            Assert.IsNotNull(newUser);
            newUser = await userManager.FindByIdAsync(newUser.Id);
            Assert.IsNotNull(newUser);
            Assert.AreEqual(newUser.Roles.Count, 2);
        }

        [Test]
        public async Task SearchUsers_WithNoFilteration_GetAllUsers()
        {
            var user = new VolunteerViewModel()
            {
                Address = "Address",
                CityId = _securityUnitOfWork.CityRepository.GetAll().First().Id,
                ConfirmPassword = "P@ssw0rd",
                Email = "searchUser@Email.com",
                FullName = "searchUser",
                Mobile = "0123456",
                Password = "P@ssw0rd",
                Roles = _roleManager.Roles.Take(2).Select(c => c.Id).ToList()
            };

            var entityResult = await _userService.AddUser(user);
            Assert.IsTrue(entityResult.Succeeded);

            SearchVolunteersViewModel searchUsersViewModel = new SearchVolunteersViewModel();
            var result = _userService.SearchUser(searchUsersViewModel);
            Assert.Greater(result.TotalCount, 0);
            Assert.GreaterOrEqual(result.Items.Count, 0);
        }

        [Test]
        public async Task UpdateUser_WithValid_Data_UpateUserAndRoles()
        {
            var user = new VolunteerViewModel()
            {
                Address = "Address",
                CityId = _securityUnitOfWork.CityRepository.GetAll().First().Id,
                ConfirmPassword = "P@ssw0rd",
                Email = "UserToUodate@Email.com",
                FullName = "UserToUpdate",
                Mobile = "0123456",
                Password = "P@ssw0rd",
                Roles = _roleManager.Roles.Take(2).Select(c => c.Id).ToList()
            };

            var entityResult = await _userService.AddUser(user);
            Assert.IsTrue(entityResult.Succeeded);

            var currentUser = await _userService.GetUser(user.Id);
            Assert.IsNotNull(currentUser);
            currentUser.Address = "NewAddress";
            currentUser.FullName = "NameAfterUpdate";
            currentUser.CityId = _securityUnitOfWork.CityRepository.GetAll().Last().Id;
            currentUser.Mobile = "0123";
            currentUser.Roles = _roleManager.Roles.Skip(2).Take(3).Select(c => c.Id).ToList();

            entityResult = await _userService.UpdateUser(currentUser);
            Assert.IsTrue(entityResult.Succeeded);

            var storedUser = await userManager.FindByIdAsync(currentUser.Id);
            Assert.IsNotNull(storedUser);
            Assert.AreEqual(storedUser.Roles.Count, 3);
        }
    }
}
