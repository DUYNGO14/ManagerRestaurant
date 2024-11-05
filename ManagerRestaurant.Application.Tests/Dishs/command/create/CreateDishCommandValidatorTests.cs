using FluentValidation.TestHelper;
using Xunit;

namespace ManagerRestaurant.Application.Dishs.command.create.Tests
{
    public class CreateDishCommandValidatorTests
    {
        [Fact()]
        public void CreateDishCommandValidatorTest_ShouldNotHaveAnyValidationErrors()
        {
            var command = new CreateDishCommand()
            {
                Name="tetsdish",
                Price=2000000,
                KiloCalories=1200
            };

            var validate = new CreateDishCommandValidator();
            var result = validate.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory()]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("ab")]
        [InlineData("abc")]
        [InlineData("abcd")]
        public void CreateDishCommandValidatorTest_ShouldHaveAnyValidationErrorsForName(string name)
        {
            var command = new CreateDishCommand() { Name=name };
            var validate = new CreateDishCommandValidator();
            var result = validate.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c=>c.Name);
        }

        [Theory()]
        [InlineData(-232)]
        public void CreateDishCommandValidatorTest_ShouldHaveAnyValidationErrorsForPrice(decimal price)
        {
            var command = new CreateDishCommand() { Price = price };
            var validate = new CreateDishCommandValidator();
            var result = validate.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.Price);
        }

        [Theory()]
        [InlineData(-1)]
        public void CreateDishCommandValidatorTest_ShouldHaveAnyValidationErrorsForKilocalor(int kilocalor)
        {
            var command = new CreateDishCommand() { KiloCalories = kilocalor };
            var validate = new CreateDishCommandValidator();
            var result = validate.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.KiloCalories);
        }
    }
}