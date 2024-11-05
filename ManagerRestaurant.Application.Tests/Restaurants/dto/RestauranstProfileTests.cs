using AutoMapper;
using FluentAssertions;
using ManagerRestaurant.Application.Restaurants.command.Create;
using ManagerRestaurant.Application.Restaurants.command.update;
using ManagerRestaurant.Domain.Entities;
using Xunit;

namespace ManagerRestaurant.Application.Restaurants.dto.Tests
{
    public class RestauranstProfileTests
    {
        private IMapper mapper;
        public RestauranstProfileTests() {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RestauranstProfile>();
            });
            mapper = config.CreateMapper();
        }
        [Fact()]
        public void CreateMap_ForRestaurantToRestaurantDto_MapCorrectly()
        {
            var restaurant = new Restaurant()
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                ContactEmail = "Test@gmail.com",
                ContactNumber="0354983740",
                Category = "VietNam",
                Address = new Address()
                {
                    Street = "test",
                    City = "test",
                }
            };

            var restaurantDto = mapper.Map<RestaurantDto>(restaurant);

            restaurantDto.Should().NotBeNull();
            restaurantDto.Id.Should().Be(restaurant.Id);
            restaurantDto.Name.Should().Be(restaurant.Name);
            restaurantDto.Description.Should().Be(restaurant.Description);
            restaurantDto.ContactEmail.Should().Be(restaurant.ContactEmail);
            restaurantDto.ContactNumber.Should().Be(restaurant.ContactNumber);
            restaurantDto.Category.Should().Be(restaurant.Category);
          
            restaurantDto.Street.Should().Be(restaurant.Address.Street);
            restaurantDto.City.Should().Be(restaurant.Address.City);
        }

        [Fact()]
        public void CreateMap_ForCreateRestaurantCommandToRestaurant_MapCorrectly()
        {
            var CreateRestaurant = new CreateRestaurantCommand()
            {
                Name = "Test",
                Description = "Test",
                ContactEmail = "Test@gmail.com",
                ContactNumber = "0354983740",
                Category = "VietNam",
                Street = "test",
                City = "test",

            };

            var restaurant = mapper.Map<Restaurant>(CreateRestaurant);

            restaurant.Should().NotBeNull();
            restaurant.Name.Should().Be(CreateRestaurant.Name);
            restaurant.Description.Should().Be(CreateRestaurant.Description);
            restaurant.ContactEmail.Should().Be(CreateRestaurant.ContactEmail);
            restaurant.ContactNumber.Should().Be(CreateRestaurant.ContactNumber);
            restaurant.Category.Should().Be(CreateRestaurant.Category);

            restaurant.Address.Street.Should().Be(CreateRestaurant.Street);
           
            restaurant.Address.City.Should().Be(CreateRestaurant.City);
        }

        [Fact()]
        public void CreateMap_ForUpdateRestaurantCommandToRestaurant_MapCorrectly()
        {
            var UpdateRestaurant = new UpdateRestaurantCommand()
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                ContactEmail = "Test@gmail.com",
                ContactNumber = "0354983740",
                Category = "VietNam",
            };

            var restaurant = mapper.Map<Restaurant>(UpdateRestaurant);

            restaurant.Should().NotBeNull();
            restaurant.Id.Should().Be(UpdateRestaurant.Id);
            restaurant.Name.Should().Be(UpdateRestaurant.Name);
            restaurant.Description.Should().Be(UpdateRestaurant.Description);
            restaurant.ContactEmail.Should().Be(UpdateRestaurant.ContactEmail);
            restaurant.ContactNumber.Should().Be(UpdateRestaurant.ContactNumber);
            restaurant.Category.Should().Be(UpdateRestaurant.Category);
        }
    }
}