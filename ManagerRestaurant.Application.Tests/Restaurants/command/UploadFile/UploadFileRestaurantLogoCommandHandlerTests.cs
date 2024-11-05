using Castle.Core.Logging;
using FluentAssertions;
using ManagerRestaurant.Domain.Entities;
using ManagerRestaurant.Domain.Exceptions;
using ManagerRestaurant.Domain.Interfaces;
using ManagerRestaurant.Domain.Respository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ManagerRestaurant.Application.Restaurants.command.UploadFile.Tests
{
    public class UploadFileRestaurantLogoCommandHandlerTests
    {
        private readonly Mock<ILogger<UploadFileRestaurantLogoCommandHandler>> _loggerMock;
        private readonly Mock<IRestaurantsRespository> _restaurantReposiotryMock;
        private readonly Mock<IBlodStoregeService> _blodStoregeServiceMock;
        private readonly UploadFileRestaurantLogoCommandHandler _handler;
        private readonly Mock<IFormFile> _formFileMock;
        public UploadFileRestaurantLogoCommandHandlerTests()
        {
            _loggerMock = new Mock<ILogger<UploadFileRestaurantLogoCommandHandler>>();
            _blodStoregeServiceMock = new Mock<IBlodStoregeService>();
            _restaurantReposiotryMock = new Mock<IRestaurantsRespository>();
            _formFileMock = new Mock<IFormFile>();
            _handler = new UploadFileRestaurantLogoCommandHandler(_loggerMock.Object, _restaurantReposiotryMock.Object, _blodStoregeServiceMock.Object);
        }

        [Fact()]
        public async Task Test_UploadFile_ReturnSuccess()
        {
            var restaurantId = 1;
            var restaurant = new Restaurant();
            var command = new UploadFileRestaurantLogoCommand
            {
                RestuarantId = restaurantId,
                File = _formFileMock.Object.OpenReadStream(),
                FilName = _formFileMock.Object.FileName,
            };
            string url = "path_file";
            _restaurantReposiotryMock.Setup(r => r.GetByIdAsync(restaurantId)).ReturnsAsync(restaurant);
            _blodStoregeServiceMock.Setup(s => s.UploadFileToBlodAsync(command.FilName, command.File)).ReturnsAsync(url);

            await _handler.Handle(command,CancellationToken.None);
            _restaurantReposiotryMock.Verify(r => r.Update(),Times.Once);
        }

        [Fact()]
        public async Task Test_UploadFile_ThrownNotFoundEception()
        {
            var restaurantId = 1;
            var restaurant = new Restaurant();
            var command = new UploadFileRestaurantLogoCommand
            {
                RestuarantId = restaurantId,
                File = _formFileMock.Object.OpenReadStream(),
                FilName = _formFileMock.Object.FileName,
            };
            string url = "path_file";
            _restaurantReposiotryMock.Setup(r => r.GetByIdAsync(restaurantId)).ReturnsAsync((Restaurant?)null);

            //act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            //assert
            //assert
            await act.Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage($"Restaurant with id : {restaurantId} doesn't exist!");
        }
    }
}