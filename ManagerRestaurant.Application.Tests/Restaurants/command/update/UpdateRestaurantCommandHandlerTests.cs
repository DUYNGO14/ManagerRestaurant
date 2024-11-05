using AutoMapper;
using FluentAssertions;
using ManagerRestaurant.Application.Users;
using ManagerRestaurant.Domain.Entities;
using ManagerRestaurant.Domain.Exceptions;
using ManagerRestaurant.Domain.Respository;
using Microsoft.Extensions.Logging;
using Moq;
using System.Security.AccessControl;
using Xunit;

namespace ManagerRestaurant.Application.Restaurants.command.update.Tests
{
    public class UpdateRestaurantCommandHandlerTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRestaurantsRespository> _respositoryMock;
        private readonly Mock<ILogger<UpdateRestaurantCommandHandler>> _loggerMock;
        private readonly Mock<IUserContext> _userContextMock;
        private readonly UpdateRestaurantCommandHandler _handler;

        public UpdateRestaurantCommandHandlerTests() 
        { 
            _mapperMock = new Mock<IMapper>();
            _respositoryMock = new Mock<IRestaurantsRespository>();
            _loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
            _userContextMock = new Mock<IUserContext>();

            _handler = new UpdateRestaurantCommandHandler(_loggerMock.Object,_respositoryMock.Object,_mapperMock.Object,_userContextMock.Object);
        }
        [Fact()]
        public async Task Test_UpdateRestaurantCommandHandler_Successfly()
        {
            var restaurantId = 1;
            var restaurant = new Restaurant() {
                Id = restaurantId,
                Name = "Test",
                Category = "VietNam",
                Description = "Test",
            };

            var command = new UpdateRestaurantCommand()
            {
                Id = restaurantId,
                Name = "Test",
                Description = "Test",
                Category="VietNam",
                ContactEmail="Test@gmail.com",
                ContactNumber="0354895654",
            };

            var currentUser = new CurrentUser("id", "test@gmail.com", []);
            _userContextMock.Setup(u=>u.GetCurrentUser()).Returns(currentUser);

            _respositoryMock.Setup(repo=>repo.GetByIdAsync(restaurantId)).ReturnsAsync(restaurant);

            //act
            await _handler.Handle(command, CancellationToken.None);
            //assert
            _respositoryMock.Verify(r => r.Update(), Times.Once());
            _mapperMock.Verify(m => m.Map(command, restaurant), Times.Once());
        }

        [Fact()]
        public async Task Test_UpdateRestaurantCommandHandler_NotFoundException()
        {
            //arrange
            var restaurantId = 2;
            var request = new UpdateRestaurantCommand()
            {
                Id = restaurantId,
            };
            var currentUser = new CurrentUser("id", "test@gmail.com", []);
            _userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

            _respositoryMock.Setup(r => r.GetByIdAsync(restaurantId)).ReturnsAsync((Restaurant?)null);

            //act
            Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

            //assert
            //assert
            await act.Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage($"Restaurant with id : {restaurantId} doesn't exist!");

        }
    }
}