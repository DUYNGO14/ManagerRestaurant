using AutoMapper;
using ManagerRestaurant.Application.Common;
using ManagerRestaurant.Application.Restaurants.dto;
using ManagerRestaurant.Application.Users;
using ManagerRestaurant.Domain.Respository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ManagerRestaurant.Application.Restaurants.queries.GetAllRestaurantForUser
{
    public class GetRestaurnatForUserQueryHandler(ILogger<GetRestaurnatForUserQueryHandler> logger,IRestaurantsRespository restaurantsRespository,IUserContext userContext,IMapper mapper) : IRequestHandler<GetRestaurnatForUserQuery, PageResult<RestaurantDto>>
    {
        public async Task<PageResult<RestaurantDto>> Handle(GetRestaurnatForUserQuery request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            logger.LogInformation("Getting all restaurants for user {userid}",user.userId);
            var (restaurnts, totalCount) = await restaurantsRespository
                .GetAllRestautrByUserId(user.userId,request.SearchPhrase,
                request.PageSize,
                request.PageNumber,
                request.SortBy,
                request.SortDirection
                );
            var restaurantDto = mapper.Map<IEnumerable<RestaurantDto>>(restaurnts);
            var pageResult = new PageResult<RestaurantDto>(restaurantDto, totalCount, request.PageSize, request.PageNumber);
            return pageResult;
        }
    }
}
