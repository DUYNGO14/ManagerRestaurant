using ManagerRestaurant.Application.Dishs.dto;
using MediatR;

namespace ManagerRestaurant.Application.Dishs.queries.getAll
{
    public class GetDishForRestaurantQuery(int restaurantId) : IRequest<IEnumerable<DishDto>>
    {
        public int RestaurantId { get; } = restaurantId;
    }
}
