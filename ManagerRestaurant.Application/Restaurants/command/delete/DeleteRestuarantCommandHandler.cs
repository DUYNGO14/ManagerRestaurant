using ManagerRestaurant.Domain.Contants;
using ManagerRestaurant.Domain.Exceptions;
using ManagerRestaurant.Domain.Respository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerRestaurant.Application.Restaurants.command.delete
{
    public class DeleteRestuarantCommandHandler(IRestaurantsRespository restaurantsRespository,
       ILogger<DeleteRestuarantCommandHandler> logger) : IRequestHandler<DeleteRestuarantCommand>
    {
        public async Task Handle(DeleteRestuarantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting restaurant with id : {@RestaurantId}", request.Id);
            var restaurant = await restaurantsRespository.GetByIdAsync(request.Id);
            if (restaurant is null) { throw new NotFoundException("Restaurant", request.Id.ToString()); }

            //if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
            //{
            //    throw new ForbidenException();
            //}
            await restaurantsRespository.Delete(restaurant);
        }
    }
}
