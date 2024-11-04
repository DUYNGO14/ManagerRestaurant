using ManagerRestaurant.Application.Common;
using ManagerRestaurant.Domain.Entities;

namespace ManagerRestaurant.Domain.Respository
{
    public interface IRestaurantsRespository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<(List<Restaurant>,int)> GetAllMatchingAsync(string? searchPhrase,int pageSize,int pagenumber,string? sortBy,SortDirection sortDirection);

        Task<(List<Restaurant>, int)> GetAllRestautrByUserId(string userId,string? searchPhrase, int pageSize, int pagenumber, string? sortBy, SortDirection sortDirection);
        Task<Restaurant> GetByIdAsync(int id);
        Task<int> Create(Restaurant restaurant);
        Task Delete(Restaurant restaurant);
        Task Update();
        
    }
}
