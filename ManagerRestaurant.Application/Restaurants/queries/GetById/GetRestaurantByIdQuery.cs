using ManagerRestaurant.Application.Restaurants.dto;
using MediatR;

namespace ManagerRestaurant.Application.Restaurants.queries.GetById
{
    public class GetRestaurantByIdQuery(int id) : IRequest<RestaurantDto>
    {
        public int Id { get; } = id;
    }
}
