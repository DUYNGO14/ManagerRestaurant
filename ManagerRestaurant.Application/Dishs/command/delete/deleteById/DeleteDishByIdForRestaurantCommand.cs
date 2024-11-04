using MediatR;

namespace ManagerRestaurant.Application.Dishs.command.delete.deleteById
{
    public class DeleteDishByIdForRestaurantCommand(int DishId, int Restaurantid) : IRequest
    {
        public int DishId { get; } = DishId;
        public int RestaurantId { get; } = Restaurantid;
    }
}
