using AutoMapper;
using ManagerRestaurant.Application.Dishs.dto;
using ManagerRestaurant.Domain.Entities;
using ManagerRestaurant.Domain.Exceptions;
using ManagerRestaurant.Domain.Respository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ManagerRestaurant.Application.Dishs.queries.getAll
{
    public class GetDishForRestaurantQueryHandler(IRestaurantsRespository restaurantsRespository,
         ILogger<GetDishForRestaurantQueryHandler> logger, IMapper mapper) : IRequestHandler<GetDishForRestaurantQuery, IEnumerable<DishDto>>
    {
        public async Task<IEnumerable<DishDto>> Handle(GetDishForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all dish");
            var restaurant = await restaurantsRespository.GetByIdAsync(request.RestaurantId);
            if (restaurant is null)
            {
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            }
            var result = mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);
            return result;
        }
    }
}
