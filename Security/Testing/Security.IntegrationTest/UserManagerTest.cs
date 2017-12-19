using System;
using NUnit.Framework;
using System.Threading.Tasks;
using FluentAssertions;

namespace Security.IntegrationTest
{
    [TestFixture]
    public class UserManagerTest
    {
        Security.AspIdentity.ApplicationUserManager userManager;

        [OneTimeSetUp]
        public void Initiate()
        {
            var dataContext = new Security.DataAccessLayer.SecurityContext();
            var securityUnitOfWork = new Security.DataAccessLayer.UnitOfWork.SecurityUnitOfWork(dataContext);
            var userStore = new AspIdentity.UserStore(securityUnitOfWork);

            userManager = new AspIdentity.ApplicationUserManager(userStore);

            var user = new AspIdentity.ApplicationUser()
            {
                UserName = "testuser",
                Email = "elkadeem1@hotmail.com"
            };

            var task = userManager.CreateAsync(user, "P@ssw0rd");
            task.Wait();
            bool isCreated = task.Result.Succeeded;
        }

        [Test]
        public async Task CreateUser_WithValidData_AddUser()
        {
            var user = new AspIdentity.ApplicationUser()
            {
                UserName = "welsaeed",
                Email = "elkadeem@hotmail.com"
            };

            var result = await userManager.CreateAsync(user, "P@ssw0rd");
            result.Succeeded.Should().Be(true);
        }

        [Test]
        public async Task FindUser_WithValidData_ReturnUser()
        {
            var currentUser = await userManager.FindAsync("testuser", "P@ssw0rd");
            currentUser.Id.Should().NotBe(Guid.Empty);
            currentUser.UserName.Should().Be("testuser");

        }

        [Test]
        public async Task FindByEmailAsync_WithValidData_ReturnUser()
        {
            var currentUser = await userManager.FindByEmailAsync("elkadeem1@hotmail.com");
            currentUser.Id.Should().NotBe(Guid.Empty);
            currentUser.Email.Should().Be("elkadeem1@hotmail.com");
        }

        [Test]
        public async Task FindByIdAsync_WithValidData_ReturnUser()
        {
            var currentUser = await userManager.FindByEmailAsync("elkadeem1@hotmail.com");

            currentUser = await userManager.FindByIdAsync(currentUser.Id);
            currentUser.Id.Should().NotBe(Guid.Empty);
            currentUser.Email.Should().Be("elkadeem1@hotmail.com");
        }

        [Test]
        public async Task FindByNameAsync_WithValidData_ReturnUser()
        {
            var currentUser = await userManager.FindByNameAsync("testuser");
            currentUser.Id.Should().NotBe(Guid.Empty);
            currentUser.Email.Should().Be("elkadeem1@hotmail.com");
        }
    }
}
