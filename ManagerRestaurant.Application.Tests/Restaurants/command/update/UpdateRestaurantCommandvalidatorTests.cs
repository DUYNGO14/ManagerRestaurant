using FluentValidation.TestHelper;
using Xunit;

namespace ManagerRestaurant.Application.Restaurants.command.update.Tests
{
    public class UpdateRestaurantCommandvalidatorTests
    {
        [Fact()]
        public void UpdateRestaurantCommandvalidatorTest_ShouldNotHaveAnyValidationErrors()
        {
            //arragne
            var command = new UpdateRestaurantCommand()
            {
                Name = "name",
                Category = "VietNam",
                ContactEmail = "test@gmail.com",
                ContactNumber = "0354983765",
            };
            var validator = new UpdateRestaurantCommandvalidator();
            //act
            var result = validator.TestValidate(command);
            //asseng
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void UpdateRestaurantCommandValidatorTest_ShouldHaveAnyValidationErrors()
        {
            //arragne
            var command = new UpdateRestaurantCommand()
            {
                Name = "na",
                Category = "VNam",
                ContactEmail = "@gmail.com",
                ContactNumber = "5465",
            };
            var validator = new UpdateRestaurantCommandvalidator();
            //act
            var result = validator.TestValidate(command);
            //asseng
            result.ShouldHaveValidationErrorFor(c => c.Name);
            result.ShouldHaveValidationErrorFor(c => c.Category);
            result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
            result.ShouldHaveValidationErrorFor(c => c.ContactNumber);
        }

        [Theory()]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("ac")]
        public void UpdateRestaurantCommandValidatorTest_ShouldHaveAnyValidationErrorsForName(string name)
        {
            //arragne
            var validator = new UpdateRestaurantCommandvalidator();
            var command = new UpdateRestaurantCommand() { Name = name };
            //act
            var result = validator.TestValidate(command);
            //assert
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Theory()]
        [InlineData("USA")]
        [InlineData("Canada")]
        [InlineData("UK")]
        [InlineData("Australia")]
        [InlineData("ThaiLan")]
        [InlineData("Lao")]
        [InlineData("VietNam")]
        [InlineData("Mexico")]
        public void UpdateRestaurantCommandValidatorTest_ShouldNotHaveAnyValidationErrorsForCategory(string category)
        {
            //arragne
            var validator = new UpdateRestaurantCommandvalidator();
            var command = new UpdateRestaurantCommand() { Category = category };
            //act
            var result = validator.TestValidate(command);
            //assert
            result.ShouldNotHaveValidationErrorFor(c => c.Category);
        }

        [Theory()]
        [InlineData("034344")]
        [InlineData("0343443432434")]
        [InlineData("0011111111")]
        [InlineData("0111111111")]
        [InlineData("0211111111")]
        [InlineData("0411111111")]
        [InlineData("0611111111")]
        public void UpdateRestaurantCommandValidatorTest_ShouldHaveAnyValidationErrorsForPhoneNumber(string phoneNumber)
        {
            //arragne
            var validator = new UpdateRestaurantCommandvalidator();
            var command = new UpdateRestaurantCommand() { ContactNumber = phoneNumber };
            //act
            var result = validator.TestValidate(command);
            //assert
            result.ShouldHaveValidationErrorFor(c => c.ContactNumber);
        }
    }
}