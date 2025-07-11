using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Validators;

public class CreateRestaurantsDtoValidator :AbstractValidator<CreateRestaurantDto>
{
    private readonly List<string> validCategories = ["Italian", "Mexican", "Japanese", "American", "Indian"];

    public CreateRestaurantsDtoValidator()
    {
        RuleFor(r=>r.Name)
            .Length(3,100)
            .WithMessage("Name field must be in the range of 3 to 100 characters");

        RuleFor(r => r.Description)
            .NotEmpty()
            .WithMessage("Description field is required");

        RuleFor(r => r.ContactEmail)
            .EmailAddress()
            .WithMessage("Please provide a valid format for Email field");

        RuleFor(r => r.Category)
            .Must(validCategories.Contains)
            //.Custom((value, context) =>
            //{
            //    var isvalidCategory=validCategories.Contains(value);
            //    if (!isvalidCategory)
            //    {
            //        context.AddFailure("Category", "Please a valid category");
            //    }
            //})
            .WithMessage("Please provide a valid category");

        RuleFor(r => r.PostalCode)
            .Matches(@"^\d{2}-\d{3}$")
            .WithMessage("Please provide a valid format for Posta Code field xx-xxx");


    }
}
