﻿using System;
using NUnit.Framework;
using FluentAssertions;
using Moq;
using Security.Domain;
using System.Threading.Tasks;
using Security.AspIdentity;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;

namespace Security.UnitTesting
{
    [TestFixture]
    public class AspIdentityTest
    {
        private Security.AspIdentity.UserStore userStore;
        private Mock<Security.Domain.ISecurityUnitOfWork> unitOfWorkMok;
        private Mock<IRoleRepository> roleRepository;
        private Mock<IUserRepository> userRepository;
        private AspIdentity.ApplicationUser user;
        private List<Role> roles;
        [SetUp]
        public void Initiate()
        {
            userRepository = new Mock<IUserRepository>();
            roleRepository = new Mock<IRoleRepository>();
            unitOfWorkMok = new Mock<Security.Domain.ISecurityUnitOfWork>();
            unitOfWorkMok.Setup<IUserRepository>(c => c.UserRepository).Returns(userRepository.Object);
            unitOfWorkMok.Setup<IRoleRepository>(c => c.RoleRepository).Returns(roleRepository.Object);

            userStore = new AspIdentity.UserStore(unitOfWorkMok.Object);

            user = new AspIdentity.ApplicationUser
            {
                Email = "elkadeem@hotmail.com",
                FullName = "elkadeem",
                UserId = new Guid("6B942923-DDAA-453F-89EC-847F0D639074"),
                UserName = "elkadeem",
            };

            roles = new List<Role> {
                new Role{RoleId  = Guid.NewGuid(), RoleName = "Role1"},
                new Role{RoleId  = Guid.NewGuid(), RoleName = "Role2"}
            };
        }

        #region IUserStore
        [Test]
        public void CreateAsync_WithNullUser_ThrowArgumentNullException()
        {
            AsyncTestDelegate action = async () => await userStore.CreateAsync(null);
            Assert.ThrowsAsync<ArgumentNullException>(action);
        }

        [Test]
        public async Task CreateAsync_WithUser_AddUserAndSave()
        {
            await userStore.CreateAsync(user);
            userRepository.Verify(c => c.Add(user), Times.Once());
            unitOfWorkMok.Verify(c => c.SaveAsync(), Times.Once);
        }

        [Test]
        public void DeleteAsync_WithNullUser_ThrowArgumentNullException()
        {
            AsyncTestDelegate action = async () => await userStore.DeleteAsync(null);
            Assert.ThrowsAsync<ArgumentNullException>(action);
        }

        [Test]
        public async Task DeleteAsync_WithUser_AddUserAndSave()
        {
            //Act
            await userStore.DeleteAsync(user);

            //Assert
            userRepository.Verify(c => c.Remove(user.Id), Times.Once());
            unitOfWorkMok.Verify(c => c.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task FindByIdAsync_WithValidUserId_ReturnUser()
        {
            //Arrange
            userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns<Guid>((a) => a == user.Id ? user : null);
            //Act
            var result = await userStore.FindByIdAsync(user.Id);
            //Assert
            result.Should().NotBeNull();
            result.UserName.Should().Be("elkadeem");

        }

        [Test]
        public async Task FindByIdAsync_WithUnValidUserId_ReturnNull()
        {
            //Arrange
            userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns<Guid>((a) => a == user.Id ? user : null);
            //Act
            var result = await userStore.FindByIdAsync(Guid.NewGuid());
            //Assert
            result.Should().BeNull();
        }

        [Test]
        public async Task FindByNameAsync_WithValidUserName_ReturnUser()
        {
            //Arrange
            userRepository.Setup(c => c.FindByUserNameAsync(It.IsAny<string>())).Returns<string>((a) => Task.FromResult((User)(a == user.UserName ? user : null)));
            //Act
            var result = await userStore.FindByNameAsync("elkadeem");
            //Assert
            result.Should().NotBeNull();
            result.UserName.Should().Be("elkadeem");

        }

        [Test]
        public async Task FindByNameAsync_WithInValidUserName_ReturnNull()
        {
            //Arrange
            userRepository.Setup(c => c.FindByUserNameAsync(It.IsAny<string>())).Returns<string>((a) => Task.FromResult((User)(a == user.UserName ? user : null)));
            //Act
            var result = await userStore.FindByNameAsync("elkadeem1");
            //Assert
            result.Should().BeNull();
        }
        #endregion

        #region IUserLoginStore
        [Test]
        public async Task AddLoginAsync_WithNullUser_ThrowArgumentNullException()
        {
            ApplicationUser user = null;
            UserLoginInfo login = null;

            try
            {
                await userStore.AddLoginAsync(user, login);
            }
            catch (ArgumentNullException ex)
            {
                ex.Should().NotBeNull();
                ex.ParamName.Should().Be("user");
            }
        }

        [Test]
        public async Task AddLoginAsync_WithNullLogin_ThrowArgumentNullException()
        {
            ApplicationUser user = new ApplicationUser();
            UserLoginInfo login = null;

            try
            {
                await userStore.AddLoginAsync(user, login);
            }
            catch (ArgumentNullException ex)
            {
                ex.Should().NotBeNull();
                ex.ParamName.Should().Be("login");
            }
        }

        [Test]
        public async Task AddLoginAsync_WithNotFoundUser_ThrowArgumentExceptionWithUser()
        {
            //Arrange
            ApplicationUser user = new ApplicationUser();
            UserLoginInfo login = new UserLoginInfo("google", "elkadeem@gmail.com");
            userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns<Guid>(null);

            try
            {
                //Act
                await userStore.AddLoginAsync(user, login);
            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.Should().NotBeNull();
                ex.ParamName.Should().Be("user");
            }
        }

        [Test]
        public async Task AddLoginAsync_WithValidUser_AddLoginToUser()
        {
            //Arrange
            UserLoginInfo login = new UserLoginInfo("google", "elkadeem@gmail.com");
            userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns(user);

            //Act
            await userStore.AddLoginAsync(user, login);

            //Assert
            user.ExternalLogins.Count.Should().Be(1);
            userRepository.Verify(c => c.Update(user), Times.Once);
            unitOfWorkMok.Verify(c => c.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task FindAsync_WithNullUserLogin_ThrowArgumentNullException()
        {
            //Arrange
            UserLoginInfo login = null;

            try
            {
                //Act
                var result = await userStore.FindAsync(login);
            }
            catch (ArgumentNullException ex)
            {
                //Assert
                ex.Should().NotBeNull();
                ex.ParamName.Should().Be("login");
            }
        }

        [Test]
        public async Task FindAsync_WithUserLogin_ReturnValidUser()
        {
            //Arrange
            UserLoginInfo login = new UserLoginInfo("google", "elkadeem@gmail.com");
            user.AddExternalLogin(login.LoginProvider, login.ProviderKey);
            userRepository.Setup(c => c.FindByLoginAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(async () => user);

            //Act
            var result = await userStore.FindAsync(login);
            //Assert
            result.Should().NotBeNull();
            result.ExternalLogins.Count.Should().Be(1);
        }

        [Test]
        public async Task GetLoginsAsync_WithNullUser_ReturnArrgmentNullExceptionOfUser()
        {
            //Arrange
            ApplicationUser applicationUser = null;

            try
            {
                //Act
                var result = await userStore.GetLoginsAsync(applicationUser);
            }
            catch (ArgumentNullException ex)
            {
                //Assert
                ex.Should().NotBeNull();
                ex.ParamName.Should().Be("user");
            }
        }

        [Test]
        public async Task GetLoginsAsync_WithNotFoundUser_ThrowArgumentExceptionWithUser()
        {
            //Arrange
            ApplicationUser user = new ApplicationUser();
            userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns<Guid>(null);

            try
            {
                //Act
                var result = await userStore.GetLoginsAsync(user);
            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.Should().NotBeNull();
                ex.ParamName.Should().Be("user");
            }
        }

        [Test]
        public async Task GetLoginsAsync_WithValidUser_ReturnValidUserLogins()
        {
            //Arrange
            UserLoginInfo login = new UserLoginInfo("google", "elkadeem@gmail.com");
            user.AddExternalLogin(login.LoginProvider, login.ProviderKey);

            login = new UserLoginInfo("facebook", "elkadeem@facebook.com");
            user.AddExternalLogin(login.LoginProvider, login.ProviderKey);

            userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns(user);

            //Act
            var result = await userStore.GetLoginsAsync(user);
            //Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(2);
        }

        [Test]
        public async Task RemoveLoginAsync_WithNullUser_ThrowArgumentNullException()
        {
            ApplicationUser user = null;
            UserLoginInfo login = null;

            try
            {
                await userStore.RemoveLoginAsync(user, login);
            }
            catch (ArgumentNullException ex)
            {
                ex.Should().NotBeNull();
                ex.ParamName.Should().Be("user");
            }
        }

        [Test]
        public async Task RemoveLoginAsync_WithNullLogin_ThrowArgumentNullException()
        {
            ApplicationUser user = new ApplicationUser();
            UserLoginInfo login = null;

            try
            {
                await userStore.RemoveLoginAsync(user, login);
            }
            catch (ArgumentNullException ex)
            {
                ex.Should().NotBeNull();
                ex.ParamName.Should().Be("login");
            }
        }

        [Test]
        public async Task RemoveLoginAsync_WithNotFoundUser_ThrowArgumentExceptionWithUser()
        {
            //Arrange
            ApplicationUser user = new ApplicationUser();
            UserLoginInfo login = new UserLoginInfo("google", "elkadeem@gmail.com");
            userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns<Guid>(null);

            try
            {
                //Act
                await userStore.RemoveLoginAsync(user, login);
            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.Should().NotBeNull();
                ex.ParamName.Should().Be("user");
            }
        }

        [Test]
        public async Task RemoveLoginAsync_WithValidUser_AddLoginToUser()
        {
            //Arrange
            UserLoginInfo login = new UserLoginInfo("google", "elkadeem@gmail.com");
            user.AddExternalLogin(login.LoginProvider, login.ProviderKey);

            UserLoginInfo loginToDelete = new UserLoginInfo("facebook", "elkadeem@facebook.com");
            user.AddExternalLogin(loginToDelete.LoginProvider, loginToDelete.ProviderKey);

            userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns(user);

            //Act
            await userStore.RemoveLoginAsync(user, loginToDelete);

            //Assert
            user.ExternalLogins.Count.Should().Be(1);
            user.ExternalLogins.Should().NotContain(c => c.LoginProvider == "facebook");
            userRepository.Verify(c => c.Update(user), Times.Once);
            unitOfWorkMok.Verify(c => c.SaveAsync(), Times.Once);
        }
        #endregion

        #region #region IUserClaimStore
        [Test]
        public async Task GetClaimsAsync_WithNullUser_ThrowArrgmentNullException()
        {
            //Arrange
            ApplicationUser user = null;

            try
            {
                //Act
                await userStore.GetClaimsAsync(user);
            }
            catch (ArgumentNullException ex)
            {
                //Assert
                ex.Should().NotBeNull();
                ex.ParamName.Should().Be("user");
            }
        }

        [Test]
        public async Task GetClaimsAsync_WithInValidUser_ThrowArrgmentException()
        {
            //Arrange
            ApplicationUser user = new ApplicationUser();

            try
            {
                //Act
                await userStore.GetClaimsAsync(user);
            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.Should().NotBeNull();
                ex.ParamName.Should().Be("user");
            }
        }

        [Test]
        public async Task GetClaimsAsync_WithInValidUser_ReturnUserCalims()
        {
            //Arrange
            user.AddClaim("Department", "Department");
            user.AddClaim("Department", "Department2");
            userRepository.Setup(c => c.GetByIDAsync(It.IsAny<Guid>())).Returns(async () => user);

            //Act
            var result = await userStore.GetClaimsAsync(user);

            //Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(2);
        }

        [Test]
        public async Task AddClaimAsync_WithNullUser_ThrowArrgmentNullException()
        {
            //Arrange
            ApplicationUser user = null;
            System.Security.Claims.Claim claim = null;
            try
            {
                //Act
                await userStore.AddClaimAsync(user, claim);
            }
            catch (ArgumentNullException ex)
            {
                //Assert
                ex.Should().NotBeNull();
                ex.ParamName.Should().Be("user");
            }
        }

        [Test]
        public async Task AddClaimAsync_WithNullClaim_ThrowArrgmentNullException()
        {
            //Arrange            
            System.Security.Claims.Claim claim = null;

            try
            {
                //Act
                await userStore.AddClaimAsync(user, claim);
            }
            catch (ArgumentNullException ex)
            {
                //Assert
                ex.Should().NotBeNull();
                ex.ParamName.Should().Be("claim");
            }
        }

        [Test]
        public async Task AddClaimAsync_WithInValidUser_ThrowArrgmentException()
        {
            //Arrange
            ApplicationUser user = new ApplicationUser();
            System.Security.Claims.Claim claim = new System.Security.Claims.Claim("Department", "Department");

            try
            {
                //Act
                await userStore.AddClaimAsync(user, claim);
            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.Should().NotBeNull();
                ex.ParamName.Should().Be("user");
            }
        }

        [Test]
        public async Task AddClaimAsync_WithInValidUser_AddClaimsToUser()
        {
            //Arrange
            System.Security.Claims.Claim claim = new System.Security.Claims.Claim("Department", "Department");
            userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns(() => user);

            //Act
            await userStore.AddClaimAsync(user, claim);

            //Assert
            user.Claims.Count.Should().Be(1);
            user.Claims.Should().OnlyContain(c => c.ClaimType == "Department");

            userRepository.Verify(c => c.Update(user), Times.Once);
            unitOfWorkMok.Verify(c => c.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task RemoveClaimAsync_WithNullUser_ThrowArrgmentNullException()
        {
            //Arrange
            ApplicationUser user = null;
            System.Security.Claims.Claim claim = null;
            try
            {
                //Act
                await userStore.RemoveClaimAsync(user, claim);
            }
            catch (ArgumentNullException ex)
            {
                //Assert
                ex.Should().NotBeNull();
                ex.ParamName.Should().Be("user");
            }
        }

        [Test]
        public async Task RemoveClaimAsync_WithNullClaim_ThrowArrgmentNullException()
        {
            //Arrange            
            System.Security.Claims.Claim claim = null;

            try
            {
                //Act
                await userStore.RemoveClaimAsync(user, claim);
            }
            catch (ArgumentNullException ex)
            {
                //Assert
                ex.Should().NotBeNull();
                ex.ParamName.Should().Be("claim");
            }
        }

        [Test]
        public async Task RemoveClaimAsync_WithInValidUser_ThrowArrgmentException()
        {
            //Arrange
            ApplicationUser user = new ApplicationUser();
            System.Security.Claims.Claim claim = new System.Security.Claims.Claim("Department", "Department");

            try
            {
                //Act
                await userStore.RemoveClaimAsync(user, claim);
            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.Should().NotBeNull();
                ex.ParamName.Should().Be("user");
            }
        }

        [Test]
        public async Task RemoveClaimAsync_WithValidUser_RemoveUserClaims()
        {
            //Arrange            
            user.AddClaim("Department", "Department");
            user.AddClaim("Department", "Department1");
            user.AddClaim("Department", "Department2");

            System.Security.Claims.Claim claimToDelete = new System.Security.Claims.Claim("Department", "Department");

            userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns(() => user);

            //Act
            await userStore.RemoveClaimAsync(user, claimToDelete);

            //Assert
            user.Claims.Count.Should().Be(2);
            user.Claims.Should().NotContain(c => c.ClaimType == "Department" && c.ClaimValue == "Department");

            userRepository.Verify(c => c.Update(user), Times.Once);
            unitOfWorkMok.Verify(c => c.SaveAsync(), Times.Once);
        }
        #endregion

        [Test, TestCaseSource(typeof(SetEmailDataTestCases), "SetEmailTestCases")]
        public void SetEmailAsync_WithAnyNullParameters_ThrowArrgmentNullException(ApplicationUser testUser
            , string testEmail)
        {
            try
            {
                //Arrange
                userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns<Guid>((a) => user.UserId == a ? user : null);
                //Act
                userStore.SetEmailAsync(testUser, testEmail);
                //Assert
                user.Email.Should().Be(testEmail);
                user.IsEmailConfirmed.Should().Be(false);
                userRepository.Verify(c => c.Update(user), Times.Once);
                unitOfWorkMok.Verify(c => c.SaveAsync(), Times.Once);
            }
            catch (ArgumentNullException ex)
            {
                //Assert
                if (testUser == null)
                    ex.ParamName.Should().Be("user");
                else
                    ex.ParamName.Should().Be("email");
            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.ParamName.Should().Be("user");
            }
        }

        [Test, TestCaseSource(typeof(SetEmailDataTestCases), "GetEmailTestCases")]
        public void GetEmailAsync_WithMultliSource(ApplicationUser testUser)
        {
            try
            {
                //Arrange
                userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns<Guid>((a) => user.UserId == a ? user : null);
                //Act
                userStore.GetEmailAsync(testUser);
                //Assert
                user.Email.Should().Be(user.Email);
            }
            catch (ArgumentNullException ex)
            {
                //Assert
                if (user == null)
                    ex.ParamName.Should().Be("user");

            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.ParamName.Should().Be("user");
            }
        }

        [Test, TestCaseSource(typeof(SetEmailDataTestCases), "GetEmailConfirmedTestCases")]
        public void GetEmailConfirmedAsync_WithMultliSource(ApplicationUser testUser)
        {
            try
            {
                user.IsEmailConfirmed = false;
                //Arrange
                userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns<Guid>((a) => user.UserId == a ? user : null);
                //Act
                userStore.GetEmailConfirmedAsync(user);
                //Assert
                user.IsEmailConfirmed.Should().Be(false);
            }
            catch (ArgumentNullException ex)
            {
                //Assert
                if (user == null)
                    ex.ParamName.Should().Be("user");

            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.ParamName.Should().Be("user");
            }
        }

        [Test, TestCaseSource(typeof(SetEmailDataTestCases), "GetEmailConfirmedTestCases")]
        public void FindByEmailAsync_WithMultliSource(ApplicationUser testUser)
        {
            try
            {
                user.IsEmailConfirmed = false;
                //Arrange
                userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns<Guid>((a) => user.UserId == a ? user : null);
                //Act
                userStore.GetEmailConfirmedAsync(user);
                //Assert
                user.IsEmailConfirmed.Should().Be(false);
            }
            catch (ArgumentNullException ex)
            {
                //Assert
                if (user == null)
                    ex.ParamName.Should().Be("user");

            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.ParamName.Should().Be("user");
            }
        }

        [Test, TestCaseSource(typeof(SetEmailDataTestCases), "FindByEmailTestCases")]
        public async Task FindByEmailAsync_Tests(string testEmail)
        {
            try
            {
                //Arrange
                userRepository.Setup(c => c.FindByEmail(It.IsAny<string>())).Returns<string>((a) => user.Email == a ? user : null);
                //Act
                var result = await userStore.FindByEmailAsync(testEmail);
                //Assert                
                result.Should().NotBeNull();
            }
            catch (ArgumentNullException ex)
            {
                //Assert
                if (user == null)
                    ex.ParamName.Should().Be("email");

            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.ParamName.Should().Be("user");
            }
        }

        [Test, TestCaseSource(typeof(SetEmailDataTestCases), "GetLockoutEndDateCases")]
        public async Task GetLockoutEndDateAsync_Tests(ApplicationUser userToTest)
        {
            try
            {
                DateTimeOffset dateTime = new DateTimeOffset(new DateTime(2010, 1, 1, 0, 0, 0, DateTimeKind.Utc));
                user.LockedOutDate = dateTime.UtcDateTime;
                //Arrange
                userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns<Guid>((a) => user.UserId == a ? user : null);
                //Act
                var result = await userStore.GetLockoutEndDateAsync(userToTest);
                //Assert                
                result.Should().BeSameDateAs(dateTime.UtcDateTime);
            }
            catch (ArgumentNullException ex)
            {
                //Assert                
                ex.ParamName.Should().Be("user");

            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.ParamName.Should().Be("user");
            }
        }


        [Test, TestCaseSource(typeof(SetEmailDataTestCases), "SetLockoutEndDateAsyncDateCases")]
        public async Task SetLockoutEndDateAsync_Tests(ApplicationUser userToTest, DateTimeOffset lockOutDate)
        {
            try
            {
                //Arrange
                userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns<Guid>((a) => user.UserId == a ? user : null);
                //Act
                await userStore.SetLockoutEndDateAsync(userToTest, lockOutDate);

                //Assert                
                if (lockOutDate == DateTimeOffset.MinValue)
                    user.LockedOutDate.Should().BeNull();
                else
                    user.LockedOutDate.Should().BeSameDateAs(lockOutDate.UtcDateTime);
            }
            catch (ArgumentNullException ex)
            {
                //Assert                
                ex.ParamName.Should().Be("user");

            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.ParamName.Should().Be("user");
            }
        }

        [Test, TestCaseSource(typeof(SetEmailDataTestCases), "UsersGenericTestCases")]
        public async Task IncrementAccessFailedCountAsync_Tests(ApplicationUser userToTest)
        {
            try
            {
                //Arrange
                userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns<Guid>((a) => user.UserId == a ? user : null);
                //Act
                var result = await userStore.IncrementAccessFailedCountAsync(userToTest);
                //Assert                
                result.Should().Be(1);
            }
            catch (ArgumentNullException ex)
            {
                //Assert                
                ex.ParamName.Should().Be("user");

            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.ParamName.Should().Be("user");
            }
        }

        [Test, TestCaseSource(typeof(SetEmailDataTestCases), "UsersGenericTestCases")]
        public async Task ResetAccessFailedCountAsync_Tests(ApplicationUser userToTest)
        {
            try
            {
                user.AccessFailedCount = 5;
                //Arrange
                userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns<Guid>((a) => user.UserId == a ? user : null);
                //Act
                await userStore.ResetAccessFailedCountAsync(userToTest);
                //Assert                
                user.AccessFailedCount.Should().Be(0);
            }
            catch (ArgumentNullException ex)
            {
                //Assert                
                ex.ParamName.Should().Be("user");

            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.ParamName.Should().Be("user");
            }
        }

        [Test, TestCaseSource(typeof(SetEmailDataTestCases), "UsersGenericTestCases")]
        public async Task SetLockoutEnabledAsync_Tests(ApplicationUser userToTest)
        {
            try
            {
                user.AccessFailedCount = 5;
                //Arrange
                userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns<Guid>((a) => user.UserId == a ? user : null);
                //Act
                await userStore.SetLockoutEnabledAsync(userToTest, true);
                //Assert                
                user.IsLocked.Should().Be(true);
            }
            catch (ArgumentNullException ex)
            {
                //Assert                
                ex.ParamName.Should().Be("user");

            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.ParamName.Should().Be("user");
            }
        }

        [Test, TestCaseSource(typeof(SetEmailDataTestCases), "UsersGenericTestCases")]
        public async Task SetPasswordHashAsync_Tests(ApplicationUser userToTest)
        {
            try
            {
                string passwordHash = "passwordHash";
                //Arrange
                userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns<Guid>((a) => user.UserId == a ? user : null);
                //Act
                await userStore.SetPasswordHashAsync(userToTest, passwordHash);
                //Assert                
                user.PasswordHash.Should().Be(passwordHash);
                userRepository.Verify(c => c.Update(user), Times.Once);
                unitOfWorkMok.Verify(c => c.SaveAsync(), Times.Once);
            }
            catch (ArgumentNullException ex)
            {
                //Assert                
                ex.ParamName.Should().Be("user");

            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.ParamName.Should().Be("user");
            }
        }

        [Test, TestCaseSource(typeof(SetEmailDataTestCases), "UsersGenericTestCases")]
        public async Task GetPasswordHashAsync_Tests(ApplicationUser userToTest)
        {
            try
            {
                user.PasswordHash = "passwordHash";
                //Arrange
                userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns<Guid>((a) => user.UserId == a ? user : null);
                //Act
                var result = await userStore.GetPasswordHashAsync(userToTest);
                //Assert                
                result.Should().Be("passwordHash");
            }
            catch (ArgumentNullException ex)
            {
                //Assert                
                ex.ParamName.Should().Be("user");

            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.ParamName.Should().Be("user");
            }
        }

        [Test, TestCaseSource(typeof(SetEmailDataTestCases), "UsersGenericTestCases")]
        public async Task HasPasswordAsync_Tests(ApplicationUser userToTest)
        {
            try
            {
                user.PasswordHash = "passwordHash";
                //Arrange
                userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns<Guid>((a) => user.UserId == a ? user : null);
                //Act
                var result = await userStore.HasPasswordAsync(userToTest);
                //Assert                
                result.Should().Be(!string.IsNullOrEmpty(user.PasswordHash));
            }
            catch (ArgumentNullException ex)
            {
                //Assert                
                ex.ParamName.Should().Be("user");

            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.ParamName.Should().Be("user");
            }
        }

        [Test, TestCaseSource(typeof(SetEmailDataTestCases), "UsersGenericTestCases")]
        public async Task SetPhoneNumberAsync_Tests(ApplicationUser userToTest)
        {
            try
            {
                string phoneNumber = "523652245";
                //Arrange
                userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns<Guid>((a) => user.UserId == a ? user : null);
                //Act
                await userStore.SetPhoneNumberAsync(userToTest, phoneNumber);
                //Assert                
                user.PhoneNumber.Should().Be(phoneNumber);

                userRepository.Verify(c => c.Update(user), Times.Once);
                unitOfWorkMok.Verify(c => c.SaveAsync(), Times.Once);
            }
            catch (ArgumentNullException ex)
            {
                //Assert                
                ex.ParamName.Should().Be("user");

            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.ParamName.Should().Be("user");
            }
        }

        [Test, TestCaseSource(typeof(SetEmailDataTestCases), "UsersGenericTestCases")]
        public async Task GetPhoneNumberAsync_Tests(ApplicationUser userToTest)
        {
            try
            {
                user.PhoneNumber = "050";
                //Arrange
                userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns<Guid>((a) => user.UserId == a ? user : null);
                //Act
                var result = await userStore.GetPhoneNumberAsync(userToTest);
                //Assert                
                result.Should().Be("050");
            }
            catch (ArgumentNullException ex)
            {
                //Assert                
                ex.ParamName.Should().Be("user");

            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.ParamName.Should().Be("user");
            }
        }

        [Test, TestCaseSource(typeof(SetEmailDataTestCases), "UsersGenericTestCases")]
        public async Task GetPhoneNumberConfirmedAsync_Tests(ApplicationUser userToTest)
        {
            try
            {
                user.IsPhoneConfirmed = true;
                //Arrange
                userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns<Guid>((a) => user.UserId == a ? user : null);
                //Act
                var result = await userStore.GetPhoneNumberConfirmedAsync(userToTest);
                //Assert                
                result.Should().Be(user.IsPhoneConfirmed);
            }
            catch (ArgumentNullException ex)
            {
                //Assert                
                ex.ParamName.Should().Be("user");

            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.ParamName.Should().Be("user");
            }
        }

        [Test, TestCaseSource(typeof(SetEmailDataTestCases), "UsersGenericTestCases")]
        public async Task SetPhoneNumberConfirmedAsync_Tests(ApplicationUser userToTest)
        {
            try
            {
                //Arrange
                userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns<Guid>((a) => user.UserId == a ? user : null);
                //Act
                await userStore.SetPhoneNumberConfirmedAsync(userToTest, true);
                //Assert                
                user.IsPhoneConfirmed.Should().Be(true);
                userRepository.Verify(c => c.Update(user), Times.Once);
                unitOfWorkMok.Verify(c => c.SaveAsync(), Times.Once);
            }
            catch (ArgumentNullException ex)
            {
                //Assert                
                ex.ParamName.Should().Be("user");

            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.ParamName.Should().Be("user");
            }
        }

        [Test, TestCaseSource(typeof(SetEmailDataTestCases), "UsersGenericTestCases")]
        public async Task GetSecurityStampAsync_Tests(ApplicationUser userToTest)
        {
            try
            {
                user.SecurityStamp = "SE";
                //Arrange
                userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns<Guid>((a) => user.UserId == a ? user : null);
                //Act
                var result = await userStore.GetSecurityStampAsync(userToTest);
                //Assert                
                result.Should().Be(user.SecurityStamp);
            }
            catch (ArgumentNullException ex)
            {
                //Assert                
                ex.ParamName.Should().Be("user");

            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.ParamName.Should().Be("user");
            }
        }

        [Test, TestCaseSource(typeof(SetEmailDataTestCases), "UsersGenericTestCases")]
        public async Task SetSecurityStampAsync_Tests(ApplicationUser userToTest)
        {
            try
            {
                string securityStamp = "SE";
                //Arrange
                userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns<Guid>((a) => user.UserId == a ? user : null);
                //Act
                await userStore.SetSecurityStampAsync(userToTest, securityStamp);
                //Assert                
                user.SecurityStamp.Should().Be(securityStamp);
                userRepository.Verify(c => c.Update(user), Times.Once);
                unitOfWorkMok.Verify(c => c.SaveAsync(), Times.Once);
            }
            catch (ArgumentNullException ex)
            {
                //Assert                
                ex.ParamName.Should().Be("user");

            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.ParamName.Should().Be("user");
            }
        }

        [Test, TestCaseSource(typeof(SetEmailDataTestCases), "UsersGenericTestCases")]
        public async Task GetTwoFactorEnabledAsync_Tests(ApplicationUser userToTest)
        {
            try
            {
                user.EnableTowFactorAuthentication = false;
                //Arrange
                userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns<Guid>((a) => user.UserId == a ? user : null);
                //Act
                var result = await userStore.GetTwoFactorEnabledAsync(userToTest);
                //Assert                
                result.Should().Be(false);
            }
            catch (ArgumentNullException ex)
            {
                //Assert                
                ex.ParamName.Should().Be("user");

            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.ParamName.Should().Be("user");
            }
        }

        [Test, TestCaseSource(typeof(SetEmailDataTestCases), "UsersGenericTestCases")]
        public async Task SetTwoFactorEnabledAsync_Tests(ApplicationUser userToTest)
        {
            try
            {
                //Arrange
                userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns<Guid>((a) => user.UserId == a ? user : null);
                //Act
                await userStore.SetTwoFactorEnabledAsync(userToTest, true);
                //Assert                
                user.EnableTowFactorAuthentication.Should().Be(true);
                userRepository.Verify(c => c.Update(user), Times.Once);
                unitOfWorkMok.Verify(c => c.SaveAsync(), Times.Once);
            }
            catch (ArgumentNullException ex)
            {
                //Assert                
                ex.ParamName.Should().Be("user");

            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.ParamName.Should().Be("user");
            }
        }

        [Test, TestCaseSource(typeof(SetEmailDataTestCases), "UsersGenericTestCases")]
        public async Task GetRolesAsync_Tests(ApplicationUser userToTest)
        {
            try
            {
                user.AddRole(new Role { RoleId = Guid.NewGuid(), RoleName = "Role1" });
                user.AddRole(new Role { RoleId = Guid.NewGuid(), RoleName = "Role2" });

                //Arrange
                userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns<Guid>((a) => user.UserId == a ? user : null);
                //Act
                var roles = await userStore.GetRolesAsync(userToTest);
                //Assert                
                roles.Count.Should().Be(2);
            }
            catch (ArgumentNullException ex)
            {
                //Assert                
                ex.ParamName.Should().Be("user");

            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.ParamName.Should().Be("user");
            }
        }

        [Test, TestCaseSource(typeof(SetEmailDataTestCases), "UserRolesGenericTestCases")]        
        public async Task AddToRoleAsync_Tests(ApplicationUser userToTest, string roleNameToTest)
        {
            try
            {
                //Arrange
                roleRepository.Setup(c => c.FindByName(It.IsAny<string>()))
                    .Returns<string>((a) => roles.FirstOrDefault(c => c.RoleName == a));
                userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns<Guid>((a) => user.UserId == a ? user : null);
                //Act
                await userStore.AddToRoleAsync(userToTest, roleNameToTest);
                //Assert                
                user.Roles.Count.Should().Be(1);
                user.Roles.Should().Contain(c => c.RoleName == "Role1");
                userRepository.Verify(c => c.Update(user), Times.Once);
                unitOfWorkMok.Verify(c => c.SaveAsync(), Times.Once);
            }
            catch (ArgumentNullException ex)
            {
                //Assert 
                if (userToTest == null)
                    ex.ParamName.Should().Be("user");
                else
                    ex.ParamName.Should().Be("roleName");

            }
            catch (ArgumentException ex)
            {
                //Assert
                if (userToTest.UserId == Guid.Empty)
                    ex.ParamName.Should().Be("user");
                else
                    ex.ParamName.Should().Be("roleName");
            }
        }

        [Test, TestCaseSource(typeof(SetEmailDataTestCases), "UserRolesGenericTestCases")]
        public async Task RemoveFromRoleAsync_Tests(ApplicationUser userToTest, string roleNameToTest)
        {
            try
            {
                //Arrange
                user.AddRole(roles[0]);
                user.AddRole(roles[1]);
                userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns<Guid>((a) => user.UserId == a ? user : null);
                //Act
                await userStore.RemoveFromRoleAsync(userToTest, roleNameToTest);
                //Assert  
                if (roles.Any(c => c.RoleName == roleNameToTest))
                {
                    user.Roles.Count.Should().Be(1);
                    user.Roles.Should().NotContain(c => c.RoleName == "Role1");
                    userRepository.Verify(c => c.Update(user), Times.Once);
                    unitOfWorkMok.Verify(c => c.SaveAsync(), Times.Once);
                }
                else
                {
                    user.Roles.Count.Should().Be(2);
                }
            }
            catch (ArgumentNullException ex)
            {
                //Assert 
                if (userToTest == null)
                    ex.ParamName.Should().Be("user");
                else if (string.IsNullOrEmpty(roleNameToTest))
                    ex.ParamName.Should().Be("roleName");

            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.ParamName.Should().Be("user");
            }
        }

        [Test, TestCaseSource(typeof(SetEmailDataTestCases), "UserRolesGenericTestCases")]
        public async Task IsInRoleAsync_Tests(ApplicationUser userToTest, string roleNameToTest)
        {
            try
            {
                //Arrange
                user.AddRole(roles[0]);
                user.AddRole(roles[1]);
                userRepository.Setup(c => c.GetByID(It.IsAny<Guid>())).Returns<Guid>((a) => user.UserId == a ? user : null);
                //Act
                var result = await userStore.IsInRoleAsync(userToTest, roleNameToTest);
                //Assert                
                result.Should().Be(roles.Any(c => c.RoleName == roleNameToTest));
            }
            catch (ArgumentNullException ex)
            {
                //Assert 
                if (userToTest == null)
                    ex.ParamName.Should().Be("user");
                else if (string.IsNullOrEmpty(roleNameToTest))
                    ex.ParamName.Should().Be("roleName");

            }
            catch (ArgumentException ex)
            {
                //Assert
                ex.ParamName.Should().Be("user");
            }
        }

    }
}
