using AutoMapper;
using ManagerRestaurant.Domain.Contants;
using ManagerRestaurant.Domain.Entities;
using ManagerRestaurant.Domain.Exceptions;
using ManagerRestaurant.Domain.Respository;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Interfaces;

namespace ManagerRestaurant.Application.Dishs.command.create
{
    public class CreateDishCommandHandler(IRestaurantsRespository restaurantsRespository, IDishRepository dishRepository,
        ILogger<CreateDishCommandHandler> logger
        , IMapper mapper
        /*, IRestaurantAuthorizationService restaurantAuthorizationService*/) : IRequestHandler<CreateDishCommand, int>
    {
        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating new dish : {@DishRequest}", request);
            var restaurant = await restaurantsRespository.GetByIdAsync(request.RestaurantId);
            if (restaurant is null)
            {
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            }
            //if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
            //{
            //    throw new ForbidenException();
            //}
            var dish = mapper.Map<Dish>(request);
            var id = await dishRepository.Create(dish);
            return id;
        }
    }
}
