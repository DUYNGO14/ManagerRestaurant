using AutoMapper;
using ManagerRestaurant.Domain.Exceptions;
using ManagerRestaurant.Domain.Respository;
using MediatR;
using Microsoft.Extensions.Logging;
namespace ManagerRestaurant.Application.Restaurants.command.update
{
    public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger,
       IRestaurantsRespository restaurantsRespository, IMapper mapper) : IRequestHandler<UpdateRestaurantCommand>
    {
        public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Update restaurant with id : {@RestaurantId} with {@Restaurant}", request.Id, request);
            var restaurant = await restaurantsRespository.GetByIdAsync(request.Id);
            if (restaurant is null)
            {
                throw new NotFoundException("Restaurant", request.Id.ToString());
            }
            mapper.Map(request, restaurant);
            await restaurantsRespository.Update();
              
        }
    }
}
