using FluentValidation;

namespace ManagerRestaurant.Application.Dishs.command.update
{
    public class UpdateDishForRestaurantCommandValidator : AbstractValidator<UpdateDishForRestaurantCommand>
    {
        public UpdateDishForRestaurantCommandValidator()
        {
            RuleFor(d => d.Price)
               .GreaterThanOrEqualTo(0)
               .WithMessage("Price must be a non-negative number!");

            RuleFor(d => d.KiloCalories)
                .GreaterThanOrEqualTo(0)
                .WithMessage("KiloCalories must be a non-negative number!");

        }
    }
}
