using FluentAssertions;
using ManagerRestaurant.Domain.Contants;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using Xunit;

namespace ManagerRestaurant.Application.Users.Tests
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUserTest_ShouldReturnCurrentUser()
        {
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, "test")  ,
                new Claim(ClaimTypes.Email, "test@gmail.com") ,
                new Claim(ClaimTypes.Role, UserRole.Admin) ,
                new Claim(ClaimTypes.Role, UserRole.Owner) ,
                new Claim(ClaimTypes.Role, UserRole.User) ,
            };
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));
            httpContextAccessor.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
            {
                User = user
            });
            var userContext = new UserContext(httpContextAccessor.Object);

            //act
            var curentuser = userContext.GetCurrentUser();
            //asert
            curentuser.Should().NotBeNull();
            curentuser.userId.Should().Be("test");
            curentuser.email.Should().Be("test@gmail.com");
            curentuser.Roles.Should().ContainInOrder(UserRole.Admin, UserRole.Owner, UserRole.User);

        }

        [Fact()]
        public void GetCurrentUserTest_ThrownInvalidOperationException()
        {
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);
            var userContext = new UserContext(httpContextAccessorMock.Object);
            //act
            Action action = () => userContext.GetCurrentUser();
            //assert
            action.Should().Throw<InvalidOperationException>()
               .WithMessage("User context is not present!");
        }

    }
}