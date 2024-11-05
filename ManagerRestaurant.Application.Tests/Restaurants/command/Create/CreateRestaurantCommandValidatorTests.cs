using FluentValidation.TestHelper;
using Xunit;

namespace ManagerRestaurant.Application.Restaurants.command.Create.Tests
{
    public class CreateRestaurantCommandValidatorTests
    {
        [Fact()]
        public void CreateRestaurantCommandValidatorTest_ShouldNotHaveAnyValidationErrors()
        {
            //arragne
            var command = new CreateRestaurantCommand()
            {
                Name = "name",
                Category="VietNam",
                ContactEmail="test@gmail.com",
                ContactNumber="0354983765",
            };
            var validator = new CreateRestaurantCommandValidator();
            //act
            var result = validator.TestValidate(command);
            //asseng
            result.ShouldNotHaveAnyValidationErrors();
        }
        [Fact()]
        public void CreateRestaurantCommandValidatorTest_ShouldHaveAnyValidationErrors()
        {
            //arragne
            var command = new CreateRestaurantCommand()
            {
                Name = "na",
                Category = "VNam",
                ContactEmail = "@gmail.com",
                ContactNumber = "5465",
            };
            var validator = new CreateRestaurantCommandValidator();
            //act
            var result = validator.TestValidate(command);
            //asseng
            result.ShouldHaveValidationErrorFor(c=>c.Name);
            result.ShouldHaveValidationErrorFor(c => c.Category);
            result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
            result.ShouldHaveValidationErrorFor(c => c.ContactNumber);
        }
        [Theory()]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("ac")]
        public void CreateRestaurantCommandValidatorTest_ShouldHaveAnyValidationErrorsForName(string name)
        {
            //arragne
            var validator = new CreateRestaurantCommandValidator();
            var command = new CreateRestaurantCommand() { Name = name };
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
        public void CreateRestaurantCommandValidatorTest_ShouldNotHaveAnyValidationErrorsForCategory(string category)
        {
            //arragne
            var validator = new CreateRestaurantCommandValidator();
            var command = new CreateRestaurantCommand() { Category = category };
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
        public void CreateRestaurantCommandValidatorTest_ShouldHaveAnyValidationErrorsForPhoneNumber(string phoneNumber)
        {
            //arragne
            var validator = new CreateRestaurantCommandValidator();
            var command = new CreateRestaurantCommand() { ContactNumber = phoneNumber };
            //act
            var result = validator.TestValidate(command);
            //assert
            result.ShouldHaveValidationErrorFor(c => c.ContactNumber);
        }
    }
}