using AutoMapper;
using FluentAssertions;
using ManagerRestaurant.Application.Restaurants.dto;
using ManagerRestaurant.Domain.Entities;
using ManagerRestaurant.Domain.Exceptions;
using ManagerRestaurant.Domain.Respository;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ManagerRestaurant.Application.Restaurants.queries.GetById.Tests
{
    public class GetRestaurantByIdQueryHandlerTests
    {
        private readonly Mock<IRestaurantsRespository> _restaurantsRespositoryMock;
        private readonly Mock<ILogger<GetRestaurantByIdQueryHandler>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetRestaurantByIdQueryHandler _handler;
        public GetRestaurantByIdQueryHandlerTests()
        {
            _restaurantsRespositoryMock = new Mock<IRestaurantsRespository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<GetRestaurantByIdQueryHandler>>();

            _handler = new GetRestaurantByIdQueryHandler(_restaurantsRespositoryMock.Object,_loggerMock.Object,_mapperMock.Object);
        }
        [Fact()]
        public async void GetRestaurantByIdQueryHandler_ReturnRestaurant()
        {
            var restaurantId = 1;
            var command = new GetRestaurantByIdQuery(restaurantId);
            var restaurant = new Restaurant() {
                Id = restaurantId,
                Name = "Test",
                Description = "Test",
                Category = "VietNam"
            };
            
            _restaurantsRespositoryMock.Setup(r=>r.GetByIdAsync(restaurantId)).ReturnsAsync(restaurant);
            await _handler.Handle(command, CancellationToken.None);
            _mapperMock.Verify(m => m.Map<RestaurantDto>(restaurant), Times.Once());
        }
        [Fact()]
        public async void GetRestaurantByIdQueryHandler_ThrownNotFoundException()
        {
            var restaurantId = 1;
            var command = new GetRestaurantByIdQuery(restaurantId);
            _restaurantsRespositoryMock.Setup(r => r.GetByIdAsync(restaurantId)).ReturnsAsync((Restaurant)null);
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