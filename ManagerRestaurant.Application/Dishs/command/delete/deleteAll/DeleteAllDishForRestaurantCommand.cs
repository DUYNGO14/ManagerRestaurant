using MediatR;

namespace ManagerRestaurant.Application.Dishs.command.delete.deleteAll
{
    public class DeleteAllDishForRestaurantCommand(int restaurantId) : IRequest
    {
        public int RestaurantId { get; } = restaurantId;
    }
}
