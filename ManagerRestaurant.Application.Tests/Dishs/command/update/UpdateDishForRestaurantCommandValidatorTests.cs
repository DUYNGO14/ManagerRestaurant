using FluentValidation.TestHelper;
using ManagerRestaurant.Application.Dishs.command.create;
using Xunit;

namespace ManagerRestaurant.Application.Dishs.command.update.Tests
{
    public class UpdateDishForRestaurantCommandValidatorTests
    {
        [Fact()]
        public void UpdateDishCommandValidatorTest_ShouldNotHaveAnyValidationErrors()
        {
            var command = new UpdateDishForRestaurantCommand()
            {
                Name = "tetsdish",
                Price = 2000000,
                KiloCalories = 1200
            };

            var validate = new UpdateDishForRestaurantCommandValidator();
            var result = validate.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory()]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("ab")]
        [InlineData("abc")]
        [InlineData("abcd")]
        public void UpdateDishCommandValidatorTest_ShouldHaveAnyValidationErrorsForName(string name)
        {
            var command = new UpdateDishForRestaurantCommand() { Name = name };
            var validate = new UpdateDishForRestaurantCommandValidator();
            var result = validate.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Theory()]
        [InlineData(-111)]
        public void UpdateDishCommandValidatorTest_ShouldHaveAnyValidationErrorsForPrice(decimal price)
        {
            var command = new UpdateDishForRestaurantCommand() { Price = price };
            var validate = new UpdateDishForRestaurantCommandValidator();
            var result = validate.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.Price);
        }

        [Theory()]
        [InlineData(-1)]
        public void UpdateDishCommandValidatorTest_ShouldHaveAnyValidationErrorsForKilocalor(int kilocalor)
        {
            var command = new UpdateDishForRestaurantCommand() { KiloCalories = kilocalor };
            var validate = new UpdateDishForRestaurantCommandValidator();
            var result = validate.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.KiloCalories);
        }
    }
}