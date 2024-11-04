using ManagerRestaurant.Domain.Contants;
using ManagerRestaurant.Domain.Entities;
using ManagerRestaurant.Domain.Exceptions;
using ManagerRestaurant.Domain.Respository;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Interfaces;

namespace ManagerRestaurant.Application.Dishs.command.delete.deleteById
{
    public class DeleteDishByIdForRestaurantCommandHandler(ILogger<DeleteDishByIdForRestaurantCommandHandler> logger,
        IRestaurantsRespository restaurantsRespository,
        IDishRepository dishRepository
       /* , IRestaurantAuthorizationService restaurantAuthorizationService*/) : IRequestHandler<DeleteDishByIdForRestaurantCommand>
    {
        public async Task Handle(DeleteDishByIdForRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting dish with id :{@dishId}, for retaurant with id : {@dishId}", request.DishId, request.RestaurantId);
            var restaurant = await restaurantsRespository.GetByIdAsync(request.RestaurantId);
            if (restaurant is null)
            {
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            }
            //if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
            //{
            //    throw new ForbidenException();
            //}
            var dish = restaurant.Dishes.SingleOrDefault(d => d.Id == request.DishId)
               ?? throw new NotFoundException(nameof(Dish), request.DishId.ToString());
            await dishRepository.Delete(dish);
        }
    }
}
