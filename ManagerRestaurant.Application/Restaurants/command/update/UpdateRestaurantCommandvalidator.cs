using FluentValidation;

namespace ManagerRestaurant.Application.Restaurants.command.update
{
    public class UpdateRestaurantCommandvalidator : AbstractValidator<UpdateRestaurantCommand>
    {
        private readonly List<string> validCategory = ["USA", "ThaiLan", "Lao", "Mexico", "VietNam"];
        public UpdateRestaurantCommandvalidator()
        {
            RuleFor(r => r.Name)
                .Length(3, 100).WithMessage("Name ");

            RuleFor(r => r.ContactEmail)
                .EmailAddress().WithMessage("Email not invalid! ");

            RuleFor(r => r.Category)
                .Must(validCategory.Contains)
                .WithMessage("Invalid category.Please choose from the valid categories!");
        }
    }
}
