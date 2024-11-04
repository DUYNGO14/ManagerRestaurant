using FluentValidation;
using ManagerRestaurant.Application.Restaurants.dto;

namespace ManagerRestaurant.Application.Restaurants.queries.GetAll
{
    public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
    {
        private int[] allowPageSize = [2, 5, 10, 15, 20, 25, 30];
        private string[] allowSortByColumnNames = [nameof(RestaurantDto.Name), nameof(RestaurantDto.Category), nameof(RestaurantDto.Description)];
        public GetAllRestaurantsQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1).WithMessage("Page number >= !1");
            RuleFor(r => r.PageSize)
                .Must(value => allowPageSize.Contains(value))
                .WithMessage($"Page size must be in [{string.Join(",", allowPageSize)}]");
            RuleFor(r => r.SortBy)
                .Must(value => allowSortByColumnNames.Contains(value))
                .When(q => q.SortBy != null)
                .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowSortByColumnNames)}]"); ;
        }
    }
}
