using FluentValidation;

namespace ManagerRestaurant.Application.Restaurants.command.Create
{
    public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        private readonly List<string> validCategory = ["USA", "Canada", "UK", "Australia", "ThaiLan", "Lao", "Mexico", "VietNam"];
        public CreateRestaurantCommandValidator()
        {
            RuleFor(r => r.Name)
                .Length(3, 100).WithMessage("Name must be longer than 3 characters.");

            RuleFor(r => r.Category)
                .Must(validCategory.Contains)
                .WithMessage("Invalid category.Please choose from the valid categories!");

            RuleFor(r => r.ContactEmail)
                .EmailAddress().WithMessage("Please provide a valid email address!");

            RuleFor(r=>r.ContactNumber)
                .Matches(@"^(03|05|07|08|09)\d{8}$").WithMessage("Phone number is invalid.");
        }
    }
}
