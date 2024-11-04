using MediatR;

namespace ManagerRestaurant.Application.Restaurants.command.UploadFile
{
    public class UploadFileRestaurantLogoCommand : IRequest
    {
        public int RestuarantId { get; set; }
        public string FilName { get; set; } = default!;
        public Stream File { get; set; } = default!;

    }
}
