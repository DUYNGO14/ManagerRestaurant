using Microsoft.AspNetCore.Identity;

namespace ManagerRestaurant.Domain.Entities
{
    public class User : IdentityUser
    {
        public DateOnly? DateOfBirth { get; set; }
        public string? Nationaly {  get; set; }

        public List<Restaurant> OwnedRestaurants { get; set; } = [];
    }
}
