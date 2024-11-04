using AutoMapper;
using ManagerRestaurant.Application.Users;
using ManagerRestaurant.Domain.Entities;
using ManagerRestaurant.Domain.Respository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ManagerRestaurant.Application.Restaurants.command.Create
{
    public class CreaterRestaurantCommandHandler(IRestaurantsRespository restaurantsRespository,
        ILogger<CreaterRestaurantCommandHandler> logger,
        IMapper mapper,IUserContext userContext) : IRequestHandler<CreateRestaurantCommand, int>
    {
        public Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();
            logger.LogInformation("creating new a restaurants {@Restaurant}",  request);
            var restaurant = mapper.Map<Restaurant>(request);
            restaurant.OwnerId = currentUser.userId;
           // restaurant.OwnerId = "f5216774-d339-419e-86ca-6f502f51551f";
            var id = restaurantsRespository.Create(restaurant);
            return id;
        }
    }
}
