using ManagerRestaurant.Domain.Contants;
using ManagerRestaurant.Domain.Entities;
using ManagerRestaurant.Domain.Exceptions;
using ManagerRestaurant.Domain.Respository;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Interfaces;

namespace ManagerRestaurant.Application.Dishs.command.delete.deleteAll
{
    public class DeleteAllDishForRestaurantCommandHandler(ILogger<DeleteAllDishForRestaurantCommandHandler> logger,
          IRestaurantsRespository restaurantsRespository,
          IDishRepository dishRepository) : IRequestHandler<DeleteAllDishForRestaurantCommand>
    {
        public async Task Handle(DeleteAllDishForRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting all dish for retaurant with id : {@dishId}", request.RestaurantId);
            var restaurant = await restaurantsRespository.GetByIdAsync(request.RestaurantId);
            if (restaurant is null)
            {
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            }

            //if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
            //{
            //    throw new ForbidenException();
            //}
            await dishRepository.DeleteAll(restaurant.Dishes);
        }
    }
}