using ManagerRestaurant.Domain.Entities;

namespace ManagerRestaurant.Domain.Respository
{
    public interface IDishRepository
    {
        Task<int> Create(Dish dish);
        Task Delete(Dish dish);
        Task DeleteAll(IEnumerable<Dish> dish);
        Task Update();
    }
}
