using Azure.Core;
using Microsoft.EntityFrameworkCore;
using ManagerRestaurant.Application.Common;
using ManagerRestaurant.Domain.Entities;
using ManagerRestaurant.Domain.Respository;
using ManagerRestaurant.Infrastructure.Persistence;
using System.Linq.Expressions;


namespace ManagerRestaurant.Infrastructure.Respository
{
    internal class RestaurantRespository(RestaurantDbContext context) : IRestaurantsRespository
    {
        public async Task<int> Create(Restaurant restaurant)
        {
            await context.AddAsync(restaurant);
            await context.SaveChangesAsync();
            return restaurant.Id;
        }

        public async Task Delete(Restaurant restaurant)
        {
            context.Restaurants.Remove(restaurant);
            await context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            var restaurants =  await context.Restaurants.ToListAsync();
            return restaurants;
        }

        public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase,int pageSize,int pageNumber,string sortBy,SortDirection sortDirection)
        {
            var search = searchPhrase?.ToLower();
            //query
            var baseQuery =  context.Restaurants
                .Where(r => search == null || r.Name.ToLower().Contains(search)
                        || r.Description.ToLower().Contains(search));
            //total items
            var totalCount = await baseQuery.CountAsync();
            // sort
            if(sortBy != null)
            {
                var columsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
                {
                    {nameof(Restaurant.Name),r=>r.Name},
                    {nameof(Restaurant.Description),r=>r.Description},
                    {nameof(Restaurant.Category),r=>r.Category},
                };
                var selectedColum = columsSelector[sortBy];
                baseQuery =sortDirection == SortDirection.Ascending
                    ? baseQuery.OrderBy(selectedColum)
                    : baseQuery.OrderByDescending(selectedColum);
            }
            //pagination
            var restaurnts =await baseQuery.Skip(pageSize * (pageNumber-1))
                 .Take(pageSize).ToListAsync();
            return (restaurnts,totalCount);
        }

        public async Task<(IEnumerable<Restaurant>, int)> GetAllRestautrByUserId(string userId, string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection)
        {
            var search = searchPhrase?.ToLower();
            //query
            var baseQuery = context.Restaurants
                .Where(r => r.OwnerId == userId);
                //.Where(r => search == null || r.Name.ToLower().Contains(search)
                //        || r.Description.ToLower().Contains(search));
            //total items
            var totalCount = await baseQuery.CountAsync();
            // sort
            if (sortBy != null)
            {
                var columsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
                {
                    {nameof(Restaurant.Name),r=>r.Name},
                    {nameof(Restaurant.Description),r=>r.Description},
                    {nameof(Restaurant.Category),r=>r.Category},
                };
                var selectedColum = columsSelector[sortBy];
                baseQuery = sortDirection == SortDirection.Ascending
                    ? baseQuery.OrderBy(selectedColum)
                    : baseQuery.OrderByDescending(selectedColum);
            }
            //pagination
            var restaurnts = await baseQuery.Skip(pageSize * (pageNumber - 1))
                 .Take(pageSize).ToListAsync();
            return (restaurnts, totalCount);
        }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            var restaurant = await context.Restaurants
                .Include(r=>r.Dishes)
                .FirstOrDefaultAsync(r=>r.Id == id);
            return restaurant;
        }

        public async Task Update()
        {
            await context.SaveChangesAsync();
        }
    }
}
