using System;
using NUnit.Framework;
using System.Threading.Tasks;
using FluentAssertions;
using Security.AspIdentity;
using System.Linq;
using Security.DataAccessLayer;
using Microsoft.AspNet.Identity;
using Security.DataAccessLayer.UnitOfWork;

namespace Security.IntegrationTest
{
    [TestFixture]
    public class UserManagerTest
    {
        private Security.AspIdentity.ApplicationUserManager userManager;
        private AspIdentity.ApplicationUser user;
        private SecurityUnitOfWork _securityUnitOfWork;

        public TestContext TestContext { get; set; }

        [OneTimeSetUp]
        public void Initiate()
        {
            var dataContext = new Security.DataAccessLayer.SecurityContext();
            _securityUnitOfWork = new Security.DataAccessLayer.UnitOfWork.SecurityUnitOfWork(dataContext);
            var userStore = new AspIdentity.UserStore(_securityUnitOfWork);

            userManager = new AspIdentity.ApplicationUserManager(userStore, null)
            {
                UserLockoutEnabledByDefault = false,
                MaxFailedAccessAttemptsBeforeLockout = 3,
                DefaultAccountLockoutTimeSpan = new TimeSpan(0, 10, 0),

            };

            user = new AspIdentity.ApplicationUser()
            {
                UserName = "defaultUser",
                Email = "defaultUser@email.com",
                Id = UserManagerTestCases.ValidUserId,
                Address = "Address",
                CityId = _securityUnitOfWork.CityRepository.GetAll().First().Id,
                DistrictId = null,
                PhoneNumber = "050",
            };

            var task = userManager.CreateAsync(user, "P@ssw0rd");
            task.Wait();
            bool isCreated = task.Result.Succeeded;
        }

        [Test]
        public async Task CreateUser_WithValidInformation_SaveUser()
        {
            var newUser = new ApplicationUser()
            {
                UserName = "elkadeem@hotmail.com",
                Email = "elkadeem@hotmail.com",
                Address = "Address",
                CityId = _securityUnitOfWork.CityRepository.GetAll().First().Id,
                DistrictId = null,// _securityUnitOfWork.DistrictRepository.GetAll().First().Id,
                PhoneNumber = "0506823646",
            };

            var result = await userManager.CreateAsync(newUser, "P@ssw0rd");
            result.Succeeded.Should().Be(true);
        }

        [Test, TestCaseSource(typeof(UserManagerTestCases), "CreateUserTestCases")]
        public async Task CreateUser(ApplicationUser user, string password, string userDescription)
        {
            IdentityResult result = await userManager.CreateAsync(user, password);

            if (userDescription == "ValidUserNameAndPassword")
            {
                result.Succeeded.Should().Be(true);
                result.Errors.Count().Should().Be(0);
            }
            else
            {
                result.Succeeded.Should().Be(false);
                result.Errors.Count().Should().BeGreaterThan(0);
            }
        }

        [Test]
        [TestCase("user1", "P@ssw0rd", TestName = "FindUser_ByValidUserNameAndPAssword_ReturnUser")]
        [TestCase("user1", "p@ssw0rd", TestName = "FindUser_ByInValidUserNameOrPAssword_ReturnNull")]
        public async Task FindUser(string userName, string password)
        {
            var currentUser = await userManager.FindAsync(userName, password);
            if (password == "P@ssw0rd")
            {
                currentUser.Should().NotBeNull();
                currentUser.Id.Should().Be(user.Id);
            }
            else
                currentUser.Should().BeNull();
        }

        [Test]
        [TestCase("user1@email.com", TestName = "FindUser_WithValidEmail_ReturnUser")]
        [TestCase("user3@email.com", TestName = "FindUser_WithInValidEmail_ReturnNull")]
        public async Task FindByEmail(string email)
        {
            var currentUser = await userManager.FindByEmailAsync(email);
            if (email == user.Email)
            {
                currentUser.Should().NotBeNull();
                currentUser.Id.Should().Be(user.Id);
            }
            else
                currentUser.Should().BeNull();

        }

        [Test]
        [TestCase("D2FAAE16-21A0-418B-A802-0582C6593C0F", TestName = "FindUser_WithValidId_ReturnUser")]
        [TestCase("B6DE8CDC-2303-4E0D-A6B4-070224F7BFAE", TestName = "FindUser_WithInValidId_ReturnNull")]
        public async Task FindByIdAsync(string Id)
        {
            Guid id = new Guid(Id);
            var currentUser = await userManager.FindByIdAsync(id);

            if (id == user.Id)
            {
                currentUser.Should().NotBeNull();
                currentUser.Id.Should().Be(user.Id);
            }
            else
                currentUser.Should().BeNull();

        }

        [Test]
        [TestCase("user1", TestName = "FindUserByName_WithValidUserName_ReturnUser")]
        [TestCase("User1", TestName = "FindUserByName_WithValidUserNameWithDifferentCase_ReturnUser")]
        [TestCase("user3", TestName = "FindUserByName_WithInValidUserName_ReturnNull")]
        public async Task FindByNameAsync(string userName)
        {
            var currentUser = await userManager.FindByNameAsync(userName);
            if (userName.ToLower() == user.UserName.ToLower())
            {
                currentUser.Should().NotBeNull();
                currentUser.Id.Should().Be(user.Id);
            }
            else
                currentUser.Should().BeNull();
        }

        [Test]
        [TestCaseSource(typeof(UserManagerTestCases), "AddUserLoginTestCases")]
        public async Task<bool> AddExternalLogin(ApplicationUser userToUpdate, UserLoginInfo userLogin)
        {
            IdentityResult result;
            ApplicationUser currentUser;
            if (userToUpdate == null)
            {
                currentUser = new ApplicationUser
                {
                    UserName = userLogin.ProviderKey,
                    Email = userLogin.ProviderKey,
                };

                result = await userManager.CreateAsync(currentUser);
                if (!result.Succeeded)
                    return result.Succeeded;
            }
            else
                currentUser = await userManager.FindByIdAsync(userToUpdate.Id);

            result = await userManager.AddLoginAsync(currentUser.Id, userLogin);
            return result.Succeeded;
        }

        [Test]
        public async Task AddUserToRole_WithValid_AddRoleToUser()
        {
            var user = await userManager.FindByIdAsync(UserManagerTestCases.ValidUserId);
            string role = "Member";
            IdentityResult identityResult = await userManager.AddToRoleAsync(user.Id, role);

            identityResult.Succeeded.Should().Be(true);
        }
    }
}
