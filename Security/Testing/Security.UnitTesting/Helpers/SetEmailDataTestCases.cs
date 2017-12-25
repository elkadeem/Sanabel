using NUnit.Framework;
using NUnit.Framework.Internal;
using Security.AspIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.UnitTesting
{
    public class SetEmailDataTestCases
    {
        public static IEnumerable<TestCaseData> SetEmailTestCases
        {
            get
            {
                yield return new TestCaseData(null, null)
                    .SetName("SetEmailAsync_WithNullUser_ThrowArgumentNullExceptionForUser");
                yield return new TestCaseData(new ApplicationUser(), null)
                    .SetName("SetEmailAsync_WithNullUser_ThrowArgumentNullExceptionForEmail");
                yield return new TestCaseData(new ApplicationUser(), "elkadeem@hotmail.com")
                    .SetName("SetEmailAsync_WithInvalidUser_ThrowArgumentExceptionForUser");
                yield return new TestCaseData(new ApplicationUser() { Id = new Guid("6B942923-DDAA-453F-89EC-847F0D639074") }
                , "elkadeem1@hotmail.com")
                .SetName("SetEmailAsync_WithValidUserAndEmail_UpdateEmail");
            }
        }

        public static IEnumerable<TestCaseData> GetEmailTestCases
        {
            get
            {
                return new List<TestCaseData> { new TestCaseData(null)
                    .SetName("GetEmailAsync_WithNullUser_ThrowArgumentNullExceptionForUser"),
                 new TestCaseData(new ApplicationUser())
                    .SetName("GetEmailAsync_WithInvalidUser_ThrowArgumentExceptionForUser"),
                new TestCaseData(new ApplicationUser() { Id = new Guid("6B942923-DDAA-453F-89EC-847F0D639074") })
                .SetName("GetEmailAsync_WithValidUserAndEmail_GetEmail")
            };
            }

        }

        public static IEnumerable<TestCaseData> GetEmailConfirmedTestCases
        {
            get
            {
                return new List<TestCaseData> { new TestCaseData(null)
                    .SetName("GetEmailConfirmedAsync_WithNullUser_ThrowArgumentNullExceptionForUser"),
                 new TestCaseData(new ApplicationUser())
                    .SetName("GetEmailConfirmedAsync_WithInvalidUser_ThrowArgumentExceptionForUser"),
                new TestCaseData(new ApplicationUser() { Id = new Guid("6B942923-DDAA-453F-89EC-847F0D639074") })
                .SetName("GetEmailConfirmedAsync_WithValidUserAndEmail_GetEmailConfirmed")
            };
            }
        }

        public static IEnumerable<TestCaseData> FindByEmailTestCases
        {
            get
            {
                return new List<TestCaseData> { new TestCaseData(null)
                    .SetName("FindByEmailAsync_WithNullEmailAddress_ThrowArgumentNullExceptionForUser"),
                new TestCaseData("elkadeem1@rrrr.com")
                    .SetName("FindByEmailAsync_WithNotExistEmailAddress_ReturnNull"),
                 new TestCaseData("elkadeem@hotmail.com")
                    .SetName("FindByEmailAsync_WithExistEmailAddress_ReturnUser")
            };
            }
        }

        public static IEnumerable<TestCaseData> GetLockoutEndDateCases
        {
            get
            {
                return new List<TestCaseData> {
                    new TestCaseData(null),
                    new TestCaseData(new ApplicationUser()).SetDescription("InvalidUser"),
                    new TestCaseData(new ApplicationUser() { Id = new Guid("6B942923-DDAA-453F-89EC-847F0D639074") })
                    .SetDescription("ValidUser")

                };
            }
        }

        public static IEnumerable<TestCaseData> SetLockoutEndDateAsyncDateCases
        {
            get
            {
                return new List<TestCaseData> {
                    new TestCaseData(null, new DateTimeOffset(2010, 1, 1, 0, 0, 0, TimeSpan.Zero)),
                    new TestCaseData(new ApplicationUser(), new DateTimeOffset(2010, 1, 1, 0, 0, 0, TimeSpan.Zero)).SetDescription("InvalidUser"),
                    new TestCaseData(new ApplicationUser(){ Id = new Guid("6B942923-DDAA-453F-89EC-847F0D639074") }
                     , DateTimeOffset.MinValue),
                    new TestCaseData(new ApplicationUser(){ Id = new Guid("6B942923-DDAA-453F-89EC-847F0D639074") }
                     , new DateTimeOffset(2010, 1, 1, 0, 0, 0, TimeSpan.Zero))
                    .SetDescription("ValidUser")

                };
            }
        }

        public static IEnumerable<TestCaseData> UsersGenericTestCases
        {
            get
            {
                return new List<TestCaseData> {
                    new TestCaseData(null),
                    new TestCaseData(new ApplicationUser(){ PasswordHash = "PasswordHash"}).SetDescription("InvalidUser"),
                    new TestCaseData(new ApplicationUser() { Id = new Guid("6B942923-DDAA-453F-89EC-847F0D639074")
                    , PasswordHash="PasswordHash"})
                    .SetDescription("ValidUser")

                };
            }
        }

        public static IEnumerable<TestCaseData> UserRolesGenericTestCases
        {
            get
            {
                return new List<TestCaseData> {
                    new TestCaseData(null, "Role1"),
                    new TestCaseData(new ApplicationUser(), "").SetDescription("Empty Roles"),
                    new TestCaseData(new ApplicationUser(), "Role1").SetDescription("InvalidUser"),
                    new TestCaseData(new ApplicationUser() { Id = new Guid("6B942923-DDAA-453F-89EC-847F0D639074") }
                    , "Role1").SetDescription("ValidUser"),
                    new TestCaseData(new ApplicationUser() { Id = new Guid("6B942923-DDAA-453F-89EC-847F0D639074") }
                    , "Role3").SetDescription("ValidUserAndInvalidRole")

                };
            }
        }
    }
}
