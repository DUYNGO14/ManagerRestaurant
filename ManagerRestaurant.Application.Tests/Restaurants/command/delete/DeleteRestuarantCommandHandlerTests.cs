using Xunit;
using ManagerRestaurant.Application.Restaurants.command.delete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Castle.Core.Logging;
using ManagerRestaurant.Domain.Respository;
using Microsoft.Extensions.Logging;
using ManagerRestaurant.Domain.Entities;
using FluentAssertions;
using ManagerRestaurant.Domain.Exceptions;

namespace ManagerRestaurant.Application.Restaurants.command.delete.Tests
{
    public class DeleteRestuarantCommandHandlerTests
    {
        private readonly Mock<ILogger<DeleteRestuarantCommandHandler>> _loggerMock;
        private readonly Mock<IRestaurantsRespository> _respositoryMock;
        private readonly DeleteRestuarantCommandHandler _handler;

        public DeleteRestuarantCommandHandlerTests()
        {
            _loggerMock = new  Mock<ILogger<DeleteRestuarantCommandHandler>>();
            _respositoryMock =new Mock<IRestaurantsRespository>();
            _handler = new DeleteRestuarantCommandHandler(_respositoryMock.Object,_loggerMock.Object);
        }

        [Fact()]
        public async void DeleteRestuarantCommandHandler_Successfly()
        {
            var restaurantId = 1;
            var command = new DeleteRestuarantCommand(restaurantId);
            var restaurant = new Restaurant()
            {
                Id = restaurantId,
                Name = "Test",
                Category = "VietNam",
                Description = "Test",
            };
            _respositoryMock.Setup(r => r.GetByIdAsync(restaurantId)).ReturnsAsync(restaurant);
            await _handler.Handle(command,CancellationToken.None);
            _respositoryMock.Verify(r => r.Delete(restaurant),Times.Once);

        }

        [Fact()]
        public async void DeleteRestuarantCommandHandler_ThrownNotFoundException()
        {
            var restaurantId = 1;
            var command = new DeleteRestuarantCommand(restaurantId);
            var restaurant = new Restaurant()
            {
                Id = restaurantId,
                Name = "Test",
                Category = "VietNam",
                Description = "Test",
            };
            _respositoryMock.Setup(r => r.GetByIdAsync(restaurantId)).ReturnsAsync((Restaurant?)null);
            //act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            //assert
            await act.Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage($"Restaurant with id : {restaurantId} doesn't exist!");

        }
    }
}