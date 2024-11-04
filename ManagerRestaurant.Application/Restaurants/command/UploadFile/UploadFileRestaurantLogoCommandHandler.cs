using ManagerRestaurant.Domain.Entities;
using ManagerRestaurant.Domain.Exceptions;
using ManagerRestaurant.Domain.Interfaces;
using ManagerRestaurant.Domain.Respository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ManagerRestaurant.Application.Restaurants.command.UploadFile
{
    public class UploadFileRestaurantLogoCommandHandler(ILogger<UploadFileRestaurantLogoCommandHandler> logger,
       IRestaurantsRespository restaurantsRespository,
       IBlodStoregeService blodStoregeService) : IRequestHandler<UploadFileRestaurantLogoCommand>
    {
        public async Task Handle(UploadFileRestaurantLogoCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Uploading restaurants logo for id : {RestaurantID}", request.RestuarantId);
            var restaurant = await restaurantsRespository.GetByIdAsync(request.RestuarantId);
            if (restaurant == null)
            {
                throw new NotFoundException(nameof(Restaurant), request.RestuarantId.ToString());
            } 
            var logoUrl = await blodStoregeService.UploadFileToBlodAsync(request.FilName, request.File);
            restaurant.LogoUrl = logoUrl;

            await restaurantsRespository.Update();
        }
    }
}
