using ManagerRestaurant.Application.Common;
using ManagerRestaurant.Application.Restaurants.dto;
using MediatR;

namespace ManagerRestaurant.Application.Restaurants.queries.GetAll
{
    public class GetAllRestaurantsQuery : IRequest<PageResult<RestaurantDto>>
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
