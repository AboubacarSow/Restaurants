using FluentValidation.TestHelper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Xunit;

namespace Restaurants.Application.Tests.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
    {
        //Arrange
        var command = new CreateRestaurantCommand
        {
            Name="Test",
            Category="Italian",
            Description="This is a delicious italian restaurant located into north of Italy",
            ContactEmail="test@test.com",
            PostalCode="12-345",
        };

        var validator = new CreateRestaurantCommandValidator();
        //Act
        var result=validator.TestValidate(command);

        //Assert
        result.ShouldNotHaveAnyValidationErrors();

    }
    [Fact()]
    public void Validator_ForValidCommand_ShouldHaveValidationErrors()
    {
        //Arrange
        var command = new CreateRestaurantCommand
        {
            Name = "sh",
            Category = "Guinea",
            Description = "",
            ContactEmail = "test@test.com",
            PostalCode = "12345",
        };

        var validator = new CreateRestaurantCommandValidator();
        //Act
        var result = validator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(c=>c.Name);
        result.ShouldHaveValidationErrorFor(c=>c.Category);
        result.ShouldHaveValidationErrorFor(c=>c.Description);
        result.ShouldHaveValidationErrorFor(c => c.PostalCode);

    }
    [Theory]
    [InlineData("Italian")]
    [InlineData("Mexican")]
    [InlineData("Japanese")]
    [InlineData("American")]
    [InlineData("Indian")]
    public void Validator_ForValidCategory_ShouldNotHaveValidationErrorForCategoryProperty(string category)
    {
        //Arrange
        var command = new CreateRestaurantCommand
        {
            Name = "Test",
            Category = category,
            Description = "This is a delicious italian restaurant located into north of Italy",
            ContactEmail = "test@test.com",
            PostalCode = "12-345",
        };
        var validator = new CreateRestaurantCommandValidator();
        // Act
        var result= validator.TestValidate(command);

        //Assert
        result.ShouldNotHaveValidationErrorFor(c => c.Category);

    }
    [Theory]
    [InlineData("Gunea")]
    [InlineData("Turkish")]
    public void Validator_ForValidCategory_ShouldHaveValidationErrorForCategoryProperty(string category)
    {
        //Arrange
        var command = new CreateRestaurantCommand
        {
            Name = "Test",
            Category = category,
            Description = "This is a delicious italian restaurant located into north of Italy",
            ContactEmail = "test@test.com",
            PostalCode = "12-345",
        };
        var validator = new CreateRestaurantCommandValidator();
        // Act
        var result= validator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(c => c.Category);

    }
    [Theory]
    [InlineData("10220")]
    [InlineData("102-20")]
    [InlineData("10 220")]
    [InlineData("10-2 20")]
    public void Validator_ForInvalidPostalCode_ShouldHaveValidationErrorsForPostalCodeProperty(string postalCode)
    {
        var command = new CreateRestaurantCommand { PostalCode = postalCode };

        var validator = new CreateRestaurantCommandValidator();
        var result= validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(r=>r.PostalCode);
    }

   

}