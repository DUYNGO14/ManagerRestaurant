using ManagerRestaurant.Application.Dishs.dto;

namespace ManagerRestaurant.Application.Restaurants.dto
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }

        public string? LogoSasUrl { get; set; }
        public List<DishDto> Dishes { get; set; } = [];
    }
}
