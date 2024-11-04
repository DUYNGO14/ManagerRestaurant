using AutoMapper;
using ManagerRestaurant.Application.Dishs.dto;
using ManagerRestaurant.Domain.Entities;
using ManagerRestaurant.Domain.Exceptions;
using ManagerRestaurant.Domain.Respository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ManagerRestaurant.Application.Dishs.queries.getById
{
    public class GetDishByIdForRestaurantQueryHandle(IRestaurantsRespository restaurantsRespository,
        ILogger<GetDishByIdForRestaurantQueryHandle> logger, IMapper mapper) : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
    {

        public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting dish with id: {@dishId} / for restaurant with id : {@restaurantId}", request.DishId, request.RestaurantId);
            var restaurant = await restaurantsRespository.GetByIdAsync(request.RestaurantId);
            if (restaurant is null)
            {
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            }
            var dish = restaurant.Dishes.SingleOrDefault(d => d.Id == request.DishId)
                ?? throw new NotFoundException(nameof(Dish), request.DishId.ToString());
            return mapper.Map<DishDto>(dish);
        }
    }
}
