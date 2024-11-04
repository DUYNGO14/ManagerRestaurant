
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ManagerRestaurant.Application.Restaurants.command.Create
{
    public class CreateRestaurantCommand : IRequest<int>
    {
       
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
    }
}
