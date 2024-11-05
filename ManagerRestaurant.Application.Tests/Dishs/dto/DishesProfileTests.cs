using AutoMapper;
using FluentAssertions;
using ManagerRestaurant.Application.Dishs.command.create;
using ManagerRestaurant.Application.Dishs.command.update;
using ManagerRestaurant.Domain.Entities;
using Xunit;

namespace ManagerRestaurant.Application.Dishs.dto.Tests
{
    public class DishesProfileTests
    {
        private readonly IMapper mapper;
        public DishesProfileTests() {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DishesProfile>();
            });

            mapper = config.CreateMapper();
        }
         
        [Fact()]
        public void CreateMap_ForDishToDishDto()
        {
            var dish = new Dish()
            {
                Id = 1,
                Name = "Test",
                Description = "Test Description",
                KiloCalories = 1,
                Price = 1,
                RestaurantId = 1,
            };

            var dishDto = mapper.Map<DishDto>(dish);
            dishDto.Should().NotBeNull();   
            dishDto.Id.Should().Be(dish.Id);
            dishDto.Name.Should().Be(dish.Name);
            dishDto.Description.Should().Be(dish.Description);
            dishDto.Price.Should().Be(dish.Price);
            dishDto.KiloCalories.Should().Be(dish.KiloCalories);
        }

        [Fact()]
        public void CreateMap_ForCreateDishCommandToDish()
        {
            var createDish = new CreateDishCommand()
            {
                Name = "Test",
                Description = "Test Description",
                KiloCalories = 1,
                Price = 1,
                RestaurantId = 1,
            };

            var dish = mapper.Map<Dish>(createDish);
            dish.Should().NotBeNull();
            dish.Name.Should().Be(createDish.Name);
            dish.Description.Should().Be(createDish.Description);
            dish.Price.Should().Be(createDish.Price);
            dish.KiloCalories.Should().Be(createDish.KiloCalories);
            dish.RestaurantId.Should().Be(createDish.RestaurantId);
        }

        [Fact()]
        public void CreateMap_ForUpdateDishCommandToDish()
        {
            var updateDish = new UpdateDishForRestaurantCommand()
            {
                Name = "Test",
                Description = "Test Description",
                KiloCalories = 1,
                Price = 1,
                RestaurantId = 1,
            };

            var dish = mapper.Map<Dish>(updateDish);
            dish.Should().NotBeNull();
            dish.Name.Should().Be(updateDish.Name);
            dish.Description.Should().Be(updateDish.Description);
            dish.Price.Should().Be(updateDish.Price);
            dish.KiloCalories.Should().Be(updateDish.KiloCalories);
            dish.RestaurantId.Should().Be(updateDish.RestaurantId);
        }

    }
}