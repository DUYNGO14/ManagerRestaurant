using AutoMapper;
using ManagerRestaurant.Application.Common;
using ManagerRestaurant.Application.Restaurants.dto;
using ManagerRestaurant.Application.Users;
using ManagerRestaurant.Domain.Respository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ManagerRestaurant.Application.Restaurants.queries.GetAll
{
    public class GetAllRestaurantsQueryHandler(IRestaurantsRespository restaurantsRespository,
       ILogger<GetAllRestaurantsQueryHandler> logger,
       IMapper mapper,IUserContext userContext) : IRequestHandler<GetAllRestaurantsQuery, PageResult<RestaurantDto>>
    {
        public async Task<PageResult<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {

            logger.LogInformation("Getting all restaurants");
            var user = userContext.GetCurrentUser();
            var (restaurnts, totalCount) = await restaurantsRespository
                                                       .GetAllMatchingAsync(request.SearchPhrase,
                                                       request.PageSize,
                                                       request.PageNumber,
                                                       request.SortBy,
                                                       request.SortDirection);
            var restaurantDto = mapper.Map<IEnumerable<RestaurantDto>>(restaurnts);
            return new PageResult<RestaurantDto>(restaurantDto, totalCount, request.PageSize, request.PageNumber);
        }
    }
}
