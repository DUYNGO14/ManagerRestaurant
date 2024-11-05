using FluentAssertions;
using ManagerRestaurant.Domain.Contants;
using Xunit;

namespace ManagerRestaurant.Application.Users.Tests
{
    public class CurrentUserTests
    {
        [Fact()]
        public void IsInRoleTest_ReturnTrue()
        {
           var currentUser = new CurrentUser("testId", "test@gmail.com", [UserRole.Admin,UserRole.Owner,UserRole.User]);
            var isRole = currentUser.IsInRole(UserRole.Admin);
            isRole.Should().BeTrue();
        }

        [Fact()]
        public void IsInRoleTest_ReturnFalse()
        {
            var currentUser = new CurrentUser("testId", "test@gmail.com", [UserRole.Owner, UserRole.User]);
            var isRole = currentUser.IsInRole(UserRole.Admin);
            isRole.Should().BeFalse();
        }
    }
}