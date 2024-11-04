using ManagerRestaurant.Application.Common;
using ManagerRestaurant.Application.Restaurants.dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerRestaurant.Application.Restaurants.queries.GetAllRestaurantForUser
{
    public class GetRestaurnatForUserQuery : IRequest<PageResult<RestaurantDto>>
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
