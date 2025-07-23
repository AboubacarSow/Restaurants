using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandValidator:AbstractValidator<UpdateRestaurantCommand>
{
    private readonly List<string> validCategories = ["Italian", "Mexican", "Japanese", "American", "Indian"];

    public UpdateRestaurantCommandValidator()
    {
        RuleFor(r => r.Id).NotEmpty().WithMessage("Please provide the restaurant Id");
        RuleFor(r => r.Name)
            .Length(3, 100)
            .WithMessage("Name field must be in the range of 3 to 100 characters");

        RuleFor(r => r.Description)
            .NotEmpty()
            .WithMessage("Description field is required");

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


    }
}

