using Microsoft.AspNet.Identity;
using NUnit.Framework;
using Sanabel.Security.Domain;
using System;
using System.Collections.Generic;

namespace Security.IntegrationTest
{
    public class UserManagerTestCases
    {
        public static Guid ValidUserId = new Guid("D2FAAE16-21A0-418B-A802-0582C6593C0F");
        public static Guid InValidId = new Guid("B6DE8CDC-2303-4E0D-A6B4-070224F7BFAE");
        public static UserLoginInfo ValidUserLogin = new UserLoginInfo("google", "user1@gmail.com");
        public static UserLoginInfo InValidUserLogin = new UserLoginInfo("google", "");

        public static IEnumerable<TestCaseData> CreateUserTestCases
        {
            get
            {
                return new List<TestCaseData> {
                    new TestCaseData(new User{ UserName = "defaultUser", Email = "user1@email.com"}, "P@ssw0rd", "ExistUser"),
                    new TestCaseData(new User{ UserName = "user2", Email = "defaultUser@email.com"}, "P@ssw0rd", "ExistEmail"),
                    new TestCaseData(new User{ UserName = "user2", Email = "user2@email.com"}, "", "EmptyPassword"),
                    new TestCaseData(new User{ UserName = "user2", Email = "user2@email.com"}, "123456", "InvalidPassword"),
                    new TestCaseData(new User{ UserName = "user2", Email = "user2@email.com"}, "P@ssw0rd", "ValidUserNameAndPassword"),
                };
            }
        }

        public static IEnumerable<TestCaseData> AddUserLoginTestCases
        {
            get
            {
                return new List<TestCaseData> {
                    new TestCaseData(null
                    , new UserLoginInfo("google", "newuser@gmail.com"))
                    .SetName("AddUserLogin_NewLogin_AddUserAndLogin")
                    .Returns(true),
                    new TestCaseData(
                        new User{ Id = ValidUserId}
                    , ValidUserLogin)
                    .SetName("AddUserLogin_ToExistingUser_AddLoginToUser")
                    .Returns(true),
                    new TestCaseData(
                        new User{ Id = ValidUserId}
                    , new UserLoginInfo("facebook", "user1@facebook.com"))
                    .SetName("AddUserLogin_SecondLogin_AddLoginToUser")
                    .Returns(true),
                    new TestCaseData(
                        new User{ Id = ValidUserId}
                    , new UserLoginInfo("facebook", "user1@facebook.com"))
                    .SetName("AddUserLogin_AddExistingLoginToExistingUser_NotAddLogin")
                    .Returns(false),
                    new TestCaseData(null, ValidUserLogin)
                    .SetName("AddUserLogin_AlreadyExistLogin_NotAddLogin")
                        .Returns(false),
                    
                };
            }
        }
    }
}
