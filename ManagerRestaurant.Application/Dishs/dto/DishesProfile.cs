using AutoMapper;
using ManagerRestaurant.Application.Dishs.command.create;
using ManagerRestaurant.Application.Dishs.command.update;
using ManagerRestaurant.Domain.Entities;

namespace ManagerRestaurant.Application.Dishs.dto
{
    public class DishesProfile : Profile
    {
        public DishesProfile()
        {
            CreateMap<UpdateDishForRestaurantCommand, Dish>();
            CreateMap<Dish, DishDto>();
            CreateMap<CreateDishCommand, Dish>();
        }
    }
}
