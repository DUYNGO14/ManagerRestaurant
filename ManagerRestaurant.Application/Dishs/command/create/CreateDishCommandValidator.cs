using FluentValidation;

namespace ManagerRestaurant.Application.Dishs.command.create
{
    public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
    {
        public CreateDishCommandValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(5).WithMessage("Name must be longer than 5 characters.");

            RuleFor(d => d.Price)
                .NotEmpty().WithMessage("Price is required.")
                .GreaterThanOrEqualTo(10000)
                .WithMessage("Price must be a non-negative number!");

            RuleFor(d => d.KiloCalories)
                .GreaterThanOrEqualTo(0)
                .WithMessage("KiloCalories must be a non-negative number!");
        }
    }
}
