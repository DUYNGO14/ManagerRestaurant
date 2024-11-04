using MediatR;

namespace ManagerRestaurant.Application.Restaurants.command.delete
{
    public class DeleteRestuarantCommand(int id) : IRequest
    {
        public int Id { get; } = id;
    }
}
