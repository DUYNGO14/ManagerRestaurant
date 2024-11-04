using ManagerRestaurant.Domain.Entities;
using ManagerRestaurant.Domain.Respository;
using ManagerRestaurant.Infrastructure.Persistence;

namespace ManagerRestaurant.Infrastructure.Respository
{
    public class DishRepository(RestaurantDbContext context) : IDishRepository
    {
        public async Task<int> Create(Dish dish)
        {
            await context.Dishes.AddAsync(dish);
            await context.SaveChangesAsync();
            return dish.Id;
        }

        public async Task Delete(Dish dish)
        {
            context.Dishes.Remove(dish);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAll(IEnumerable<Dish> dish)
        {
            context.Dishes.RemoveRange(dish);
            await context.SaveChangesAsync();
        }

        public async Task Update()
        {
            await context.SaveChangesAsync();
        }
    }
}
