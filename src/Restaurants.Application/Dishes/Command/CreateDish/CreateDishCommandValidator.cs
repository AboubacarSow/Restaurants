using FluentValidation;

namespace Restaurants.Application.Dishes.Command.CreateDish;

public class CreateDishCommandValidator :AbstractValidator<CreateDishCommand>
{
    public CreateDishCommandValidator()
    {
        RuleFor(d => d.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price must be greater than zero (0)");  

        RuleFor(d => d.KiloCalories)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Kilocalories must be greater than zero (0)");
    }
}
