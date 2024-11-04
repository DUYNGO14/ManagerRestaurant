using ManagerRestaurant.Application.Dishs.dto;
using MediatR;

namespace ManagerRestaurant.Application.Dishs.queries.getById
{
    public class GetDishByIdForRestaurantQuery(int dishId, int restaurantId) : IRequest<DishDto>
    {
        public int DishId { get; } = dishId;
        public int RestaurantId { get; } = restaurantId;
    }
}
