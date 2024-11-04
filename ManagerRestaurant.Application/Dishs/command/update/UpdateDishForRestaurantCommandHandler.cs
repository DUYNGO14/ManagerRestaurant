using AutoMapper;
using ManagerRestaurant.Domain.Entities;
using ManagerRestaurant.Domain.Exceptions;
using ManagerRestaurant.Domain.Respository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ManagerRestaurant.Application.Dishs.command.update
{
    public class UpdateDishForRestaurantCommandHandler(ILogger<UpdateDishForRestaurantCommandHandler> logger, IDishRepository dishRepository, IRestaurantsRespository restaurantsRespository, IMapper mapper) : IRequestHandler<UpdateDishForRestaurantCommand>
    {
        public async Task Handle(UpdateDishForRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating dish with id {@dishId} for restaurant with id : {@restaurantID}", request.DishId, request.RestaurantId);
            var restaurant = await restaurantsRespository.GetByIdAsync(request.RestaurantId);
            if (restaurant is null)
            {
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            }
            var dish = restaurant.Dishes.SingleOrDefault(d => d.Id == request.DishId)
                ?? throw new NotFoundException(nameof(Dish), request.DishId.ToString());
            var updateDish = mapper.Map(request, dish);
            await dishRepository.Update();
        }
    }
}
