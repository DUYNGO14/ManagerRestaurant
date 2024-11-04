using AutoMapper;
using ManagerRestaurant.Application.Restaurants.dto;
using ManagerRestaurant.Domain.Exceptions;
using ManagerRestaurant.Domain.Interfaces;
using ManagerRestaurant.Domain.Respository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ManagerRestaurant.Application.Restaurants.queries.GetById
{
    public class GetRestaurantByIdQueryHandler(IRestaurantsRespository restaurantsRespository,
        ILogger<GetRestaurantByIdQueryHandler> logger,
        IMapper mapper) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
    {
        public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting restaurnt by id with {@ResturantId}", request.Id);
            var r = await restaurantsRespository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException("Restaurant", request.Id.ToString());
            var restaurantDto = mapper.Map<RestaurantDto>(r);
            return restaurantDto;
        }
    }
}
