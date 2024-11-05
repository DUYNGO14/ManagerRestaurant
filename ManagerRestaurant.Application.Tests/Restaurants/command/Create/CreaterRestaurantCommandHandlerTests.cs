using AutoMapper;
using Castle.Core.Logging;
using FluentAssertions;
using ManagerRestaurant.Application.Users;
using ManagerRestaurant.Domain.Contants;
using ManagerRestaurant.Domain.Entities;
using ManagerRestaurant.Domain.Respository;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ManagerRestaurant.Application.Restaurants.command.Create.Tests
{
    public class CreaterRestaurantCommandHandlerTests
    {
        [Fact()]
        public async Task Test_CreaterRestaurantCommandHandler_ReturnRestaurantId()
        {
            //arrage
            var loggerMock = new Mock<ILogger<CreaterRestaurantCommandHandler>>();
            var mapperMock = new Mock<IMapper>();

            var command = new CreateRestaurantCommand();
            var resturant = new Restaurant();
            mapperMock.Setup(m=>m.Map<Restaurant>(command)).Returns(resturant);

            var restaurantsRespositoryMock = new Mock<IRestaurantsRespository>();
            restaurantsRespositoryMock.Setup(repo=>repo.Create(It.IsAny<Restaurant>())).ReturnsAsync(1);

            var currentUser = new CurrentUser("ownerId", "owner@gmail.com", [UserRole.Owner]);
            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(u=>u.GetCurrentUser()).Returns(currentUser);

            var commandHandler = new CreaterRestaurantCommandHandler(
                restaurantsRespositoryMock.Object,loggerMock.Object,mapperMock.Object, userContextMock.Object);

            //act 
            var result = await commandHandler.Handle(command,CancellationToken.None);
            //asert
            result.Should().Be(1);
            resturant.OwnerId.Should().Be("ownerId");
            restaurantsRespositoryMock.Verify(r => r.Create(resturant), Times.Once);
        }
    }
}