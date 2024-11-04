using AutoMapper;
using ManagerRestaurant.Application.Restaurants.command.Create;
using ManagerRestaurant.Application.Restaurants.command.update;
using ManagerRestaurant.Domain.Entities;

namespace ManagerRestaurant.Application.Restaurants.dto
{
    public class RestauranstProfile : Profile
    {
        public RestauranstProfile()
        {
            CreateMap<UpdateRestaurantCommand, Restaurant>();
            CreateMap<CreateRestaurantCommand, Restaurant>()
                .ForMember(d => d.Address, opt => opt.MapFrom(src => new Address()
                {
                    City = src.City,
                    Street = src.Street,
                }))
                ;
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(d => d.City, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
                .ForMember(d => d.Street, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
                .ForMember(d => d.Dishes, opt => opt.MapFrom(src => src.Dishes));
        }

    }
}
