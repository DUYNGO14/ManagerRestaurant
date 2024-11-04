
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ManagerRestaurant.Application.Restaurants.command.Create
{
    public class CreateRestaurantCommand : IRequest<int>
    {
        [Required(ErrorMessage ="Name is required!")]
        public string Name { get; set; } = default!;
        [Required(ErrorMessage = "Description is required!")]
        public string Description { get; set; } = default!;
        [Required(ErrorMessage = "Name is required!")]
        public string Category { get; set; } = default!;
        [EmailAddress(ErrorMessage = "Email is not valid format!!")]
        public string? ContactEmail { get; set; }
        [RegularExpression(@"^(03|05|07|08|09)\d{8}$", ErrorMessage = "Phone number is invalid.")]
        public string? ContactNumber { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
    }
}
